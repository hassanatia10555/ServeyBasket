using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.JwtOptions
{
    public class JwtOptionsValidator : AbstractValidator<JwtOptions>
    {
        public JwtOptionsValidator()
        {
            RuleFor(x => x.Audience)
                .NotEmpty().WithMessage("Audience Must Has Value");

            RuleFor(x => x.Issuer)
                .NotEmpty().WithMessage("Issuer Must Has Value");

            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Key Must Has Value")
                .MinimumLength(32)
                .WithMessage("Key must be at least 32 characters long.");


            RuleFor(x => x.ExpirationMinutes)
                .GreaterThan(0)
                .WithMessage("ExpirationMinutes must be greater than zero.");
        }
    }
}
