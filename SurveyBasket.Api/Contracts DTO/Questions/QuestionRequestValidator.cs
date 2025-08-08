namespace SurveyBasket.Api.ContractsDTO.Questions
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Content)
               .NotEmpty()
               .Length(3, 100);

            RuleFor(x => x.Answers)
               .NotNull();

            RuleFor(x => x.Answers)
                .NotEmpty()
                .Must(x => x.Count > 1)
                .WithMessage("Question should has at least 2 answers")
                .When(x => x.Answers != null);


            RuleFor(x => x.Answers)
                .NotEmpty()
                .Must(x => x.Count == x.Distinct().Count())
               .WithMessage("You cannot add duplicated answers for the same question")
                  .When(x => x.Answers != null);


        }

    }
}
