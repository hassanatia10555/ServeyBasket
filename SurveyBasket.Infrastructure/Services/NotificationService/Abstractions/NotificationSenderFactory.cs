using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;

namespace SurveyBasket.Infrastructure.Services.NotificationService.Abstractions
{
    internal class NotificationSenderFactory(IServiceProvider serviceProvider) : INotificationSenderFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public INotificationSender Create(NotificationChannel channel)
        {
            return
                _serviceProvider.GetKeyedService<INotificationSender>(channel) ??
                throw new ArgumentException($"Cant Resolve {channel} Not Registered !");
        }
    }
}
