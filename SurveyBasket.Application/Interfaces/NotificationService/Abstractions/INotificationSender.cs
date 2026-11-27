namespace SurveyBasket.Application.Interfaces.NotificationService.Abstractions
{
    public interface INotificationSender
    {
        public Task<bool> SendAsync(string sendTo, string message, string subject);
    }
}
