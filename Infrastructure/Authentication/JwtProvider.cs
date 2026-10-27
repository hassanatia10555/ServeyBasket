using Core.Contracts.JwtOptions;
using Core.Interfaces.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public (string Token, int Expiration) GenerateJwtToken(ApplicationUser user)
        {
            var now = DateTime.UtcNow;

            var expiration = now.AddMinutes(
                _jwtOptions.ExpirationMinutes);

            Claim[] claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName)
            ];

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return (tokenString, (token.ValidTo - DateTime.UtcNow).Minutes);
        }

        public string? ValidateJwtToken( string token)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = _jwtOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken)
                    return null;

                var userId = jwtToken.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)
                    ?.Value;

                if (userId is null)
                    return null;

                return userId;
            }
            catch 
            {
                return null;
            }            
        }
    }
}
