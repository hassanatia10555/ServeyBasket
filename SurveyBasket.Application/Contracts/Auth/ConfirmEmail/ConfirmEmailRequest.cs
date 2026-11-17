namespace SurveyBasket.Application.Contracts.Auth.ConfirmEmail
{
    public record ConfirmEmailRequest(
        string UserId,
        string Code
        );
}
