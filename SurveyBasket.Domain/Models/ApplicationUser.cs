namespace SurveyBasket.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<RefreshToken> RefreshTokens = [];
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
