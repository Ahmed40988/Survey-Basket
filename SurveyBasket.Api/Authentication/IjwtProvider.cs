namespace SurveyBasket.Api.Authentication
{
    public interface IjwtProvider
    {
        (String Token, int expiresin) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
        string? ValidateToken(string token);
    }
}
