using SurveyBasket.Api.Contracts.Votes;
using SurveyBasket.Api.Error;

namespace SurveyBasket.Api.Services.Service;

public class VoteService(ApplicationDbcontext context) : IVoteService
{
    private readonly ApplicationDbcontext _context = context;

    public async Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken = default)
    {
        var hasVote = await _context.Votes.AnyAsync(x => x.PollId == pollId && x.UserId == userId, cancellationToken);

        if (hasVote)
            return Result.Failure(VoteErrors.DuplicatedVote);

        var pollIsExists = await _context.polls.AnyAsync(x => x.Id == pollId && x.IsPubliched && x.Startsat <= DateOnly.FromDateTime(DateTime.UtcNow) && x.Endsat >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

        if (!pollIsExists)
            return Result.Failure(PollErrors.PollNotFound);

        var availableQuestions = await _context.Questions
            .Where(x => x.PollId == pollId && x.IsActive)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var questionid = request.Answers.Select(x => x.QuestionId);

        if (!questionid.SequenceEqual(availableQuestions))
            return Result.Failure(VoteErrors.InvalidQuestions);

        var vote = new Vote
        {
            PollId = pollId,
            UserId = userId,
            VoteAnswers = request.Answers.Adapt<IEnumerable<VoteAnswer>>().ToList()
        };

        await _context.AddAsync(vote, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}