namespace SurveyBasket.ContractsDTO.Authentication;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);