using SurveyBasket.Application.Contracts.TemplateModels;
using SurveyBasket.Application.Interfaces.NotificationService;
using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;
using SurveyBasket.Domain.Constants;

namespace SurveyBasket.Application.Services
{
    internal class PollNotificationService(
        IApplicationDbContext applicationDbContext,
        IIdentityService identityService,
        INotificationSenderFactory notificationSender,
        IMessageBodyBuilderFactory confirmationEmailMessage) : IPollNotificationService
    {
        private readonly IApplicationDbContext _context = applicationDbContext;
        private readonly IIdentityService _identityService = identityService;
        private readonly INotificationSenderFactory _notificationSender = notificationSender;
        private readonly IMessageBodyBuilderFactory _emailMessage = confirmationEmailMessage;

        public async Task SendNewPollNotificationAsync(string? pollId = null)
        {
            IEnumerable<Poll> polls = [];

            if (pollId is not null)
            {
                var poll = await _context.Set<Poll>().SingleOrDefaultAsync(
                    x => x.Id == pollId && x.IsPublished);

                polls = [poll!];
            }
            else
            {

                var today = DateTime.UtcNow.Date;

                polls = await _context.Set<Poll>()
                    .Where(
                    x => x.IsPublished &&
                    x.StartsAt!.Value.Date == today)
                    .AsNoTracking()
                    .ToListAsync();
            }

            var users = await _identityService.FindAllAsync(
                x => x.EmailConfirmed);

            var builder =
                _emailMessage.Create<NewPollEmailTemplateModel>(
                    NotificationChannel.Email,
                    NotificationMessageType.NewPoll);

            var sender =
                _notificationSender.Create(NotificationChannel.Email);

            foreach (var poll in polls)
            {
                foreach (var user in users)
                {
                    var model = new NewPollEmailTemplateModel(
                        $"{user.FirstName} {user.LastName}",
                        poll.Title,
                        poll.Summary,
                        "url");

                    var message = builder.Build(model);

                    await sender.SendAsync(
                                     user.Email!,
                                      message
                                      , "📣 New Poll Created ");
                }
            }
        }
    }
}
