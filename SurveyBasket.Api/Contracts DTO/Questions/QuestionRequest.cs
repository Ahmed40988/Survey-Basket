namespace SurveyBasket.Api.ContractsDTO.Questions
{
    public record QuestionRequest(

        string Content,
        List<string> Answers
        );
}
