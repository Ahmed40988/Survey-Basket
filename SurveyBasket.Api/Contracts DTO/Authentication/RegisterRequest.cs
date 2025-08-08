namespace SurveyBasket.Api.Contracts_DTO.Authentication
{
    public record RegisterRequest(
    string Email,
    string Password,
    string Fname,
    string Lname
        );
}
