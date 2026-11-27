using System.Linq.Expressions;

namespace SurveyBasket.Infrastructure.Services.Authentication
{
    public class IdentityService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext context) : IIdentityService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<SignInResult> PasswordSignInAsync(
            ApplicationUser user,
            string password,
            bool isPersistent,
            bool lockoutOnFailure)
            =>await signInManager.PasswordSignInAsync(
                user, password, isPersistent, lockoutOnFailure);

        public async Task<IdentityResult> CreateAsync(
            ApplicationUser user,
            string password)
            =>await userManager.CreateAsync(user, password);

        public async Task<ApplicationUser?> FindByEmailAsync(string email)
            =>await userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser?> FindByIdAsync(string userId)
            =>await userManager.FindByIdAsync(userId);

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
            => await userManager.UpdateAsync(user);

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
            =>await userManager.DeleteAsync(user);

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
            =>await userManager.GenerateEmailConfirmationTokenAsync(user);

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
            =>await userManager.ConfirmEmailAsync(user, token);

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
            =>await userManager.IsEmailConfirmedAsync(user);

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
            => await userManager.Users.ToListAsync();

            public async Task<IEnumerable<ApplicationUser>> FindAllAsync(
                    Expression<Func<ApplicationUser, bool>> expression,
                    CancellationToken cancellationToken = default)
            {
                    return await _context.Set<ApplicationUser>()
                        .Where(expression)
                        .ToListAsync(cancellationToken);
            }
    }
}
