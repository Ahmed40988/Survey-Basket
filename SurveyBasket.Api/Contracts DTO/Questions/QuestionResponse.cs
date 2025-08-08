using SurveyBasket.Api.ContractsDTO.Answers;

namespace SurveyBasket.Api.ContractsDTO.Questions
{
    public record QuestionResponse(
       int Id,
       string Content,
       IEnumerable<AnswerResponse> Answers
   );
}
