using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Auth
{
    public interface IRefreshTokenService
    {
        public Task<(DateTime expirationOn,string token)?> AssignRefreshTokenAsync(ApplicationUser userId, CancellationToken cancellationToken);
        public Task<ApplicationUser?> ValidateRefreshTokenAsync(string id ,string Token , CancellationToken cancellationToken);
        public Task<bool> RevokeRefreshTokenAsync(string userId , string Token, CancellationToken cancellationToken);
    }
}
