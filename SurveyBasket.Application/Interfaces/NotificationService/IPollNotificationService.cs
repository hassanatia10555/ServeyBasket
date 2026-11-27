namespace SurveyBasket.Application.Interfaces.NotificationService
{
    public interface IPollNotificationService
    {
        Task SendNewPollNotificationAsync(string? pollId = null);
    }
}
