namespace Core.Contracts.Auth.Login
{
    public record LoginResponse(
        string Email,
        string Password,
        string Token,
        int ExpireOn,
        string RefrishToken
        );

}
