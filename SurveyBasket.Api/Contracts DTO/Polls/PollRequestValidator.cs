
namespace SurveyBasket.Api.Contracts_DTO.Polls
{
    //abo gamal
    public class LoginRequestValidator : AbstractValidator<PollRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.Summary)
                .Length(1, 1500)
                .WithMessage("Num of Char's Description is Between 1 To 1500 Char's ");

            RuleFor(x => x.Startsat)
                 .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(x => x.Endsat)
                .NotEmpty();

            RuleFor(x => x).Must(HasVaildDates)
                .WithName(nameof(PollRequest.Endsat))
                .WithMessage($"End date is must Greater than  or Equals start date ");


        }

        private bool HasVaildDates(PollRequest Request)
        {
            return Request.Endsat >= Request.Startsat;
        }
    }
}
