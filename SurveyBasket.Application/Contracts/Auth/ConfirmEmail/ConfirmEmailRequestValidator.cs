namespace SurveyBasket.Application.Contracts.Auth.ConfirmEmail
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Confirmation code is required.");
        }
    }
}

