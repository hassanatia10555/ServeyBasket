using SurveyBasket.Domain.Constants;
namespace SurveyBasket.Application.Interfaces.NotificationService.Abstractions
{
    public interface INotificationSenderFactory
    {
        public INotificationSender Create(NotificationChannel channel);

    }
}
