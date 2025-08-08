using SurveyBasket.Api.ContractsDTO.Results;

namespace SurveyBasket.Api.ContractsDTO.Result
{
    public record VoteResponse(
    string VoterName,
    DateTime VoteDate,
    IEnumerable<QuestionAnswerResponse> SelectedAnswers
        );
}
