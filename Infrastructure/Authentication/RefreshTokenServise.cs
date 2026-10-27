using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Auth;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authentication
{

    public class RefreshTokenService(
        UserManager<ApplicationUser> userManager,
        ILogger<RefreshTokenService> logger) : IRefreshTokenService
                                            
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<RefreshTokenService> _logger = logger;
        private const int RefreshTokenExpirationDays = 14;

        public async Task<(DateTime expirationOn, string token)?> AssignRefreshTokenAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresOn = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays)
            };

            user.RefreshTokens.Add(refreshToken);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError(
                  string.Join(", ",
                         result.Errors.Select(x => x.Description)));

                return null;
            }

            return (refreshToken.ExpiresOn, refreshToken.Token);
        }

        public async Task<ApplicationUser?> ValidateRefreshTokenAsync(string id ,string token ,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _userManager.FindByIdAsync(id);

            if (result is null)
                return null!;

            var checkToken = result.RefreshTokens.SingleOrDefault(x => x.Token == token && x.IsActive);

            if (checkToken is null)
                return null!;           

            return result;
        }

        public async Task<bool> RevokeRefreshTokenAsync(string userId, string token, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validateToken = await ValidateRefreshTokenAsync(userId, token ,cancellationToken);

            if (validateToken is null)
                return false;

            var checkToken = validateToken.RefreshTokens.SingleOrDefault(x => x.Token == token && x.IsActive);

            checkToken!.RevokedOn = DateTime.UtcNow; // null save becouse i am already chescked on null in "ValidateRefreshTokenAsync"

            await _userManager.UpdateAsync(validateToken);

            return true;
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }
    }
}
