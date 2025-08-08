
using SurveyBasket.Api.Contracts.Votes;

namespace SurveyBasket.Api.Services.Iservice;

public interface IVoteService
{
    Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken = default);
}