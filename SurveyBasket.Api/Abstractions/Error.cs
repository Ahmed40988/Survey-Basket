namespace SurveyBasket.Api.Abstractions
{
    public record Error(string code, string Descriptions, int? StatusCode)
    {
        public static readonly Error None = new(string.Empty, string.Empty, null);
    }
}
