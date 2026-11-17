using SurveyBasket.Domain.Constants;

namespace SurveyBasket.Application.Contracts.Auth.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("User Name is required.")
                .EmailAddress().WithMessage("Pleas Enter Valid Email Address !..");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(RegexPatterns.Password).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}
