namespace Core.Contracts.Auth.Login
{
    public record LoginResponse(
        string UserId,
        string Email,
        string Password,
        string JwtToken,
        int JwtExpiration,
        string RefrishToken,
        DateTime ExpireOn
        );

}
