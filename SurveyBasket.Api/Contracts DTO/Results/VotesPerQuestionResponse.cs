namespace SurveyBasket.Api.ContractsDTO.Results
{
    public record VotesPerQuestionResponse(
     string Question,
     IEnumerable<VotesPerAnswerResponse> SelectedAnswers
 );
}
