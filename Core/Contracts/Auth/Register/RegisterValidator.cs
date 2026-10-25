using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Contracts.Auth.Register
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x=>x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            
            RuleFor(x=>x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Must(BePassword).WithMessage("Pleas Match The Password Constraints");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");
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
