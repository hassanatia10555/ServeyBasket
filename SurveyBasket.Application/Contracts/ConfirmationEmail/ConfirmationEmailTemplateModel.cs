namespace SurveyBasket.Application.Contracts.ConfirmationEmail
{
    public record ConfirmationEmailTemplateModel(
        string UserName,
        string ConfirmationLink,
        string ApplicationName,
        int Year
        );
}
