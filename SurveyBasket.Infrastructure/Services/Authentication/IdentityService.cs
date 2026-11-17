namespace SurveyBasket.Infrastructure.Services.Authentication
{
    public class IdentityService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) : IIdentityService
    {
        public Task<SignInResult> PasswordSignInAsync(
            ApplicationUser user,
            string password,
            bool isPersistent,
            bool lockoutOnFailure)
            => signInManager.PasswordSignInAsync(
                user, password, isPersistent, lockoutOnFailure);

        public Task<IdentityResult> CreateAsync(
            ApplicationUser user,
            string password)
            => userManager.CreateAsync(user, password);

        public Task<ApplicationUser?> FindByEmailAsync(string email)
            => userManager.FindByEmailAsync(email);

        public Task<ApplicationUser?> FindByIdAsync(string userId)
            => userManager.FindByIdAsync(userId);

        public Task<IdentityResult> UpdateAsync(ApplicationUser user)
            => userManager.UpdateAsync(user);

        public Task<IdentityResult> DeleteAsync(ApplicationUser user)
            => userManager.DeleteAsync(user);

        public Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
            => userManager.GenerateEmailConfirmationTokenAsync(user);

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
            => userManager.ConfirmEmailAsync(user, token);

        public Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
            => userManager.IsEmailConfirmedAsync(user);
    }
}
