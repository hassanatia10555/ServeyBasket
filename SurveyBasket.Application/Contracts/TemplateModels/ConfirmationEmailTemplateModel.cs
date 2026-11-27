namespace SurveyBasket.Application.Contracts.TemplateModels
{
    public record ConfirmationEmailTemplateModel(
        string UserName,
        string ConfirmationLink,
        string ApplicationName,
        int Year
        );
}
