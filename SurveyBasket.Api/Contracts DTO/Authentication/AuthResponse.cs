namespace SurveyBasket.Api.Contracts_DTO.Authentication
{
    public record AuthResponse(
        string id,
        string? Email,
        string Fname,
        string Lname,
        string Token,
        int Expiresin,
        string RefreshToken,
        DateTime RefreshTokenExpiration
        );
}
