namespace SurveyBasket.Api.ContractsDTO.Users.Validators
{
    public class UpdatedProfileRequestValidator : AbstractValidator<UpdatedProfileRequest>
    {
        public UpdatedProfileRequestValidator()
        {

            RuleFor(x => x.Fname)
              .NotEmpty()
              .Length(3, 100);

            RuleFor(x => x.Lname)
              .NotEmpty()
              .Length(3, 100);

        }

    }
}
