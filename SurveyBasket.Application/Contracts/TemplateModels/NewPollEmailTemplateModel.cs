namespace SurveyBasket.Application.Contracts.TemplateModels
{
    public record NewPollEmailTemplateModel
    (
         string UserName,
         string PollTitle,
         string PollSummary,
         string PollUrl
    );
}
