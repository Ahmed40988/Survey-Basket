namespace SurveyBasket.Api.Contracts_DTO.Authentication
{
    public record LoginRequest(
        string Email,
        string Password
        );
}
