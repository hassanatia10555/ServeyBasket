using SurveyBasket.Domain.Constants;

namespace SurveyBasket.Application.Interfaces.NotificationService.Abstractions
{
    public interface IMessageBodyBuilderFactory
    {
        IMessageBodyBuilder<TModel> Create<TModel>(
            NotificationChannel notificationChannel,
            NotificationMessageType notificationMessageType);
    }
}
