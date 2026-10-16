
using Core.Contracts.Poll;

namespace Core.Validation.Poll;

public class PollRequestValidator : AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Title is required.")
            .MaximumLength(100)
                .WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Summary)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Summary is required.")
            .MaximumLength(500)
                .WithMessage("Summary must not exceed 500 characters.");

        RuleFor(x => x.StartsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage(string.Format("Start date must be greater than {0}", DateTime.Today));

        RuleFor(x => x.EndsAt)
            .NotEmpty();

        RuleFor(x => x)
            .Must(HasValidDates)
            .WithName(nameof(PollRequest.EndsAt))
            .WithMessage("End date must be greater than start date.");
    }

    private static bool HasValidDates(PollRequest request)
    {
        return request.StartsAt < request.EndsAt;
    }
}

