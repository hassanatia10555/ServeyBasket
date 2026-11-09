using FluentValidation;

namespace SurveyBasket.Application.Contracts.Vote
{
    public class VoteRequestValidator : AbstractValidator<VoteRequest>
    {
        public VoteRequestValidator()
        {
            RuleFor(x => x.VoteAnswerRequests)
                .NotEmpty()
                .WithMessage("At least one vote answer is required.");

            RuleFor(x => x.VoteAnswerRequests)
                .Must(answers =>
                    answers.Select(a => a.QuestionId)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .Count() == answers.Count)
                .WithMessage("Cannot have duplicate questions in a single vote.")
                .When(x => x.VoteAnswerRequests is not null &&
                           x.VoteAnswerRequests.All(a =>
                               !string.IsNullOrWhiteSpace(a.QuestionId)));
        }
    }
}