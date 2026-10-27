using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Auth
{
    public interface IJwtProvider
    {
        public (string Token, int Expiration) GenerateJwtToken(ApplicationUser user);
        public string? ValidateJwtToken(string Token);
    }
}
