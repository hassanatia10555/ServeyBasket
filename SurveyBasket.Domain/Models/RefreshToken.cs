namespace SurveyBasket.Domain.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime ExpiresOn { get; set; }

        public DateTime? RevokedOn { get; set; }

        public bool IsActive => DateTime.UtcNow < ExpiresOn && RevokedOn is null ;
    }
}
