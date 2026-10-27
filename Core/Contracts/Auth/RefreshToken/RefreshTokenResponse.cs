using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Auth.RefreshToken
{
    public record RefreshTokenResponse(

        string RefreshToken,

        DateTime RefreshTokenExpirationDate,

        string JwtToken,

        int JwtExpireOn
    );
}
