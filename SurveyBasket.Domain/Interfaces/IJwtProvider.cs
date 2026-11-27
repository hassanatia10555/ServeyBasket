namespace SurveyBasket.Domain.Interfaces
{
    public interface IJwtProvider
    {
        (string Token, int Expiration) GenerateJwtToken(ApplicationUser user);
        string? ValidateJwtToken(string Token);
    }
}
