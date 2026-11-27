using SurveyBasket.Application.Contracts.TemplateModels;
using SurveyBasket.Infrastructure.Services.EmailService.Abstractions.Email;

namespace SurveyBasket.Infrastructure.Services.EmailService.ConfirmationEmail
{
    public class ConfirmationEmailMessageBodyBuilder
        : EmailMessageBodyBuilder<ConfirmationEmailTemplateModel>
    {
        protected override string TemplateName => "ConfirmationEmail";
    }
}
