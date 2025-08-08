using SurveyBasket.Api.ContractsDTO.Result;

namespace SurveyBasket.Api.Contracts_DTO.Result
{
    public record PollVotesResponse(
        string Title,
        IEnumerable<VoteResponse> Votes

        );
}
