using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Authentication
{
    public interface IJwtProvider
    {
        (string Token, int Expiration) GenerateToken(ApplicationUser user);
    }
}
