namespace SurveyBasket.Api.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Fname { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
        public bool IsDisabled { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = [];

    }
}
