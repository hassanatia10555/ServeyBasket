namespace SurveyBasket.Application.Contracts.Auth.Register
{
    public record RegisterRequest(
        string Email,
        string Password,
        string FirstName,
        string LastName
    );
}
