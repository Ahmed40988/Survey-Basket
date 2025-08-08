namespace SurveyBasket.Api.Contracts_DTO.Responses
{
    public record PollResponse(int Id, string Title, string Summary, bool IsPubliched, DateOnly Startsat, DateOnly Endsat);
}
