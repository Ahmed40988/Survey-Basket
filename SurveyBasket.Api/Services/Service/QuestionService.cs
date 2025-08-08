using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.ContractsDTO.Answers;
using SurveyBasket.Api.ContractsDTO.Questions;
using SurveyBasket.Api.Error;
using System.Linq.Dynamic.Core;

namespace SurveyBasket.Api.Services.Service
{
    public class QuestionService(ApplicationDbcontext context, ICacheService cacheService, ILogger<QuestionService> logger) : IQuestionService
    {
        private readonly ApplicationDbcontext _context = context;
        private readonly ICacheService _cacheService = cacheService;
        private readonly ILogger<QuestionService> _logger = logger;
        private const string _cachePrefix = "availableQuestions";
        public async Task<Result<PaginatedList<QuestionResponse>>> GetAllAsync(int PollId, RequestFilters filters, CancellationToken cancellationToken = default)
        {
            var PollisExist = await _context.polls.AnyAsync(x => x.Id == PollId, cancellationToken);

            if (!PollisExist)
                return Result.Failure<PaginatedList<QuestionResponse>>(PollErrors.PollNotFound);

            var query = _context.Questions
        .Where(x => x.PollId == PollId);

            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(x => x.Content.Contains(filters.SearchValue));
            }

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }

            var source = query
                            .Include(x => x.Answers)
                            .ProjectToType<QuestionResponse>()
                            .AsNoTracking();

            var questions = await PaginatedList<QuestionResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);
            return Result.Success(questions);
        }


        public async Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollId, string UserId, CancellationToken cancellationToken = default)
        {
            var HasVote = await _context.Votes.AnyAsync(v => v.Id == PollId && v.UserId == UserId, cancellationToken);

            if (HasVote)
                return Result.Failure<IEnumerable<QuestionResponse>>(VoteErrors.DuplicatedVote);

            var PollisExist = await _context.polls.AnyAsync(x => x.Id == PollId && x.IsPubliched && x.Startsat <= DateOnly.FromDateTime(DateTime.UtcNow) && x.Endsat >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

            if (!PollisExist)
                return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound);

            var cacheKey = $"{_cachePrefix}-{PollId}";
            IEnumerable<QuestionResponse> questions = [];
            var CachQuesetions = await _cacheService.GetAsync<IEnumerable<QuestionResponse>>(cacheKey, cancellationToken);
            if (CachQuesetions is null)
            {
                questions = await _context.Questions
                    .Where(q => q.PollId == PollId && q.IsActive)
                    .Include(x => x.Answers)
                    .Select(q => new QuestionResponse(
                        q.Id,
                        q.Content,
                        q.Answers.Where(a => a.IsActive)
                        .Select(a => new AnswerResponse(a.Id, a.Content)
                    )))
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                await _cacheService.SetAsync(cacheKey, questions, cancellationToken);

            }
            else
            {
                questions = CachQuesetions;

            }
            return Result.Success<IEnumerable<QuestionResponse>>(questions);
        }
        public async Task<Result<QuestionResponse>> GetAsync(int PollId, int id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions
                .Where(x => x.PollId == PollId && x.Id == id)
                .Include(x => x.Answers)
                .ProjectToType<QuestionResponse>()
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (question is null)
                return Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound);

            return Result.Success<QuestionResponse>(question);
        }


        public async Task<Result<QuestionResponse>> AddAsync(int PollId, QuestionRequest request, CancellationToken cancellationToken)
        {
            var PollisExist = await _context.polls.AnyAsync(x => x.Id == PollId, cancellationToken);

            if (!PollisExist)
                return Result.Failure<QuestionResponse>(PollErrors.PollNotFound);

            var questionIsDuplicated = await _context.Questions.AnyAsync(x => x.PollId == PollId && request.Content == x.Content, cancellationToken);

            if (questionIsDuplicated)
                return Result.Failure<QuestionResponse>(QuestionErrors.DuplicatedQuestionContent);

            var question = request.Adapt<Question>();
            question.PollId = PollId;

            await _context.Questions.AddAsync(question, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync($"{_cachePrefix}-{PollId}", cancellationToken);
            return Result.Success(question.Adapt<QuestionResponse>());
        }


        public async Task<Result> UpdateAsync(int pollid, int id, QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var ExistingQuestion = await _context.Questions.AnyAsync(p => p.PollId == pollid
            && p.Content == request.Content
            && p.Id != id, cancellationToken);
            if (ExistingQuestion)
                return Result.Failure<PollResponse>(QuestionErrors.DuplicatedQuestionContent);

            var question = await _context.Questions.Include(x => x.Answers).SingleOrDefaultAsync(x => x.PollId == pollid && x.Id == id, cancellationToken);

            if (question is null)
                return Result.Failure(QuestionErrors.QuestionNotFound);

            question.Content = request.Content;

            // Answers is exist now in database
            var currentAnswers = question.Answers.Select(x => x.Content).ToList();

            // Answers is not exist in database
            var newAnswers = request.Answers.Except(currentAnswers).ToList();

            foreach (var answer in newAnswers)
                question.Answers.Add(new Answer { Content = answer });


            //if Any Answer in database not exist in Rrquest Answers ==> change isactive false
            //mean is deleted as not send in updated reqyest 
            foreach (var answer in question.Answers)
                answer.IsActive = request.Answers.Contains(answer.Content);

            await _context.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync($"{_cachePrefix}-{pollid}", cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ToggleStatusAsync(int pollid, int id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(x => x.PollId == pollid && x.Id == id, cancellationToken);

            if (question is null)
                return Result.Failure(QuestionErrors.QuestionNotFound);

            question.IsActive = !question.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync($"{_cachePrefix}-{pollid}", cancellationToken);
            return Result.Success();
        }

    }
}
