namespace SurveyBasket.Application.Contracts.Auth.RefreshToken
{
    public record RefreshTokenResponse(

        string RefreshToken,

        DateTime RefreshTokenExpirationDate,

        string JwtToken,

        int JwtExpireOn
    );
}
