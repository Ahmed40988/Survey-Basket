using SurveyBasket.Api.Contracts.Users;

namespace SurveyBasket.Api.Contracts_DTO.Users.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
           .NotEmpty()
           .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");

            RuleFor(x => x.Fname)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Lname)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Roles)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Roles)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("You cannot add duplicated role for the same user")
                .When(x => x.Roles != null);

        }
    }
}
