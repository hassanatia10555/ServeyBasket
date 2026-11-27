using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;

namespace SurveyBasket.Infrastructure.Services.NotificationService.Abstractions
{
    internal class MessageBodyBuilderFactory(IServiceProvider serviceProvider) : IMessageBodyBuilderFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public IMessageBodyBuilder<TModel> Create<TModel>(
            NotificationChannel notificationChannel,
            NotificationMessageType notificationMessageType)
        {
            var key =
                (notificationChannel, notificationMessageType, typeof(TModel));

            return
                _serviceProvider.GetKeyedService<IMessageBodyBuilder<TModel>>(key) ??
                throw new ArgumentException($"Cant Resolve {typeof(TModel)} Not Registered !");
        }
    }
}
