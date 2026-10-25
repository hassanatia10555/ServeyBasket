using System.Text.RegularExpressions;

namespace Core.Contracts.Auth.Login
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
                .Must(BePassword).WithMessage("Pleas Match The Password Constraints");
                            
        }
        private static bool BePassword(string pass) 
        {
            if (string.IsNullOrWhiteSpace(pass))
                return false;

            return pass.Length >= 6
                    && Regex.IsMatch(pass, @"[A-Z]")
                    && Regex.IsMatch(pass, @"[a-z]") 
                    && Regex.IsMatch(pass, @"\d") 
                    && Regex.IsMatch(pass, @"[^a-zA-Z0-9]");
        }
    }
}
