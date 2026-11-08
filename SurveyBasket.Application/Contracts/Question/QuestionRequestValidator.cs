namespace SurveyBasket.Application.Contracts.Question
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Answers)
                .NotNull()
                .WithMessage("Answers are required.");

            RuleFor(x => x.Answers)
                .Must(answers => answers!.Count >= 2)
                .WithMessage("A question must have at least two answers.")
                .When(x => x.Answers is not null);

            RuleForEach(x => x.Answers)
                .ChildRules(answer =>
                {
                    answer.RuleFor(x => x.Content)
                        .NotEmpty()
                        .MaximumLength(200);
                });

            RuleFor(x => x.Answers)
                .Must(answers =>
                    answers.Select(a => a.Content.Trim())
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .Count() == answers.Count)
                .WithMessage("An answer can only appear once in the request.")
                .When(x => x.Answers is not null &&
                           x.Answers.All(a => !string.IsNullOrWhiteSpace(a.Content)));
        }
    }
}
