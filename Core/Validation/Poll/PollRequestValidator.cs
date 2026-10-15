using Core.Contracts.Poll;

namespace Core.Validation.Poll;

public class PollRequestValidator: AbstractValidator<PollRequest>
{

    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .When(x=>x.Title is not null)
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .When(x=>x.Description is not null)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
    }
}

