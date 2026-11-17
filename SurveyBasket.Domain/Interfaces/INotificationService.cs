namespace SurveyBasket.Domain.Interfaces
{
    public interface INotificationService
    {
        Task NotifyAsync(string message, string subject, string toEmail);
    }
}
