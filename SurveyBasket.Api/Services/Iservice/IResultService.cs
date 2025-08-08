using SurveyBasket.Api.Contracts_DTO.Result;
using SurveyBasket.Api.Contracts_DTO.Results;
using SurveyBasket.Api.ContractsDTO.Results;

namespace SurveyBasket.Api.Services.Iservice
{
    public interface IResultService
    {
        Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollID, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<VotesPerDayResponse>>> GetPollVotesPerDayAsync(int PollID, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetPollVotesPerQuestionAsync(int PollID, CancellationToken cancellationToken = default);
    }
}
