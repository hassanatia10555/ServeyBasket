namespace SurveyBasket.Application.Contracts.Auth.ResendConfirmationEmail
{
    public class ResendConfirmationEmailRequestValidator : AbstractValidator<ResendConfirmationEmailRequest>
    {
        public ResendConfirmationEmailRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Please enter field {PropertyName} !")
                .EmailAddress()
                .WithMessage("Please enter a valid email address !");
        }
    }
}

