using SurveyBasket.Api.Contracts_DTO.Result;
using SurveyBasket.Api.Contracts_DTO.Results;
using SurveyBasket.Api.ContractsDTO.Result;
using SurveyBasket.Api.ContractsDTO.Results;
using SurveyBasket.Api.Error;

namespace SurveyBasket.Api.Services.Service
{
    public class ResultService(ApplicationDbcontext context) : IResultService
    {
        private readonly ApplicationDbcontext _context = context;

        public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollID, CancellationToken cancellationToken = default)
        {
            var PollVotes = await _context.polls
                .Where(x => x.Id == PollID)
                .Select(p => new PollVotesResponse(
                    p.Title,
                    p.Votes.Select(v => new VoteResponse(
                        $"{v.User.Fname} {v.User.Lname}",
                        v.SubmittedOn,
                        v.VoteAnswers.Select(a => new QuestionAnswerResponse(
                            a.Question.Content,
                            a.Answer.Content
                            ))
                        ))
                     )).SingleOrDefaultAsync(cancellationToken);

            return PollVotes is null
      ? Result.Failure<PollVotesResponse>(PollErrors.PollNotFound)
      : Result.Success(PollVotes);
        }

        public async Task<Result<IEnumerable<VotesPerDayResponse>>> GetPollVotesPerDayAsync(int PollID, CancellationToken cancellationToken = default)
        {
            var pollIsExists = await _context.polls.AnyAsync(x => x.Id == PollID, cancellationToken: cancellationToken);

            if (!pollIsExists)
                return Result.Failure<IEnumerable<VotesPerDayResponse>>(PollErrors.PollNotFound);

            var VotesPerDay = await _context.Votes
                 .Where(v => v.PollId == PollID)
                 .GroupBy(v => new { Date = DateOnly.FromDateTime(v.SubmittedOn) })
                 .Select(g => new VotesPerDayResponse(
                     g.Key.Date,
                     g.Count()

                     ))
                 .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<VotesPerDayResponse>>(VotesPerDay);


        }


        public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetPollVotesPerQuestionAsync(int PollID, CancellationToken cancellationToken = default)
        {
            var pollIsExists = await _context.polls.AnyAsync(x => x.Id == PollID, cancellationToken: cancellationToken);

            if (!pollIsExists)
                return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(PollErrors.PollNotFound);

            var votesPerQuestion = await _context.VoteAnswers
             .Where(x => x.Vote.PollId == PollID)
             .Select(x => new VotesPerQuestionResponse(
                 x.Question.Content,
                 x.Question.Votes
                     .GroupBy(x => new { AnswerId = x.Answer.Id, AnswerContent = x.Answer.Content })
                     .Select(g => new VotesPerAnswerResponse(
                         g.Key.AnswerContent,
                         g.Count()
                     ))
             ))
             .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<VotesPerQuestionResponse>>(votesPerQuestion);



        }
    }
}
