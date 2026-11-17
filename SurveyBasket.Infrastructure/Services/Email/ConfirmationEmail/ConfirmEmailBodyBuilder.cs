using SurveyBasket.Application.Contracts.ConfirmationEmail;

namespace SurveyBasket.Infrastructure.Services.Email.ConfirmationEmail
{
    public class ConfirmationEmailBodyBuilder
        : EmailBodyBuilder<ConfirmationEmailTemplateModel>
    {
        protected override string TemplateName => "ConfirmationEmail";
    }
}
