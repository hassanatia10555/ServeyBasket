using SurveyBasket.Application.Contracts.TemplateModels;
using SurveyBasket.Infrastructure.Services.EmailService.Abstractions.Email;

namespace SurveyBasket.Infrastructure.Services.EmailService
{
    public class NewPollEmailMessageBodyBuilder :
        EmailMessageBodyBuilder<NewPollEmailTemplateModel>
    {
        protected override string TemplateName => "NewPollEmail";
    }
}
