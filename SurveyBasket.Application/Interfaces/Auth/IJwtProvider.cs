namespace SurveyBasket.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        (string Token, int Expiration) GenerateJwtToken(ApplicationUser user);
        string? ValidateJwtToken(string Token);
    }
}
