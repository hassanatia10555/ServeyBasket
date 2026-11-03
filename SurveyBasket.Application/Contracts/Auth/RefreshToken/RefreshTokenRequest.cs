namespace SurveyBasket.Application.Contracts.Auth.RefreshToken
{
    public record RefreshTokenRequest(
                
        string OldRefreshToken,
        
        string OldJwtToken
        
    );
  
}
