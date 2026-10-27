using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Auth.RefreshToken
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.OldJwtToken)
                .NotEmpty()
                .WithMessage("Pleas Enter Field {PropertyName} !");

            RuleFor(x => x.OldRefreshToken)
                .NotEmpty()
                .WithMessage("Pleas Enter Field {PropertyName} !");

        }
    }
}
