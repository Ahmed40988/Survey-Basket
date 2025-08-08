namespace SurveyBasket.Api.Contracts_DTO.Authentication
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexPatterns.Password);

            RuleFor(x => x.Fname)
              .NotEmpty()
              .Length(3, 100);

            RuleFor(x => x.Lname)
              .NotEmpty()
              .Length(3, 100);

        }

    }
}
