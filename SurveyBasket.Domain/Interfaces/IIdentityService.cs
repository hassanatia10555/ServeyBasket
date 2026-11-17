namespace SurveyBasket.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(
            ApplicationUser user,
            string password,
            bool isPersistent,
            bool lockoutOnFailure);

        Task<IdentityResult> CreateAsync(ApplicationUser user,string password);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<ApplicationUser?> FindByIdAsync(string userId);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
    }
}
