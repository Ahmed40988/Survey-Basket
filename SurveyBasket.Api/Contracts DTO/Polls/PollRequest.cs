namespace SurveyBasket.Api.Contracts_DTO.Requests
{
    public record PollRequest
        (
        string Title,
        string Summary,
        DateOnly Startsat,
        DateOnly Endsat
        );
}
