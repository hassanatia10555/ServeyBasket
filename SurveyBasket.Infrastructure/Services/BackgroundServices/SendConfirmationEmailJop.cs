using Hangfire;
using SurveyBasket.Domain.Constants;
using SurveyBasket.Infrastructure.Services.NotificationService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Infrastructure.Services.BackgroundServices
{
    //internal class SendConfirmationEmailJop(
    //            INotificationSenderFactory notificationSenderFactory) : ISendConfirmationEmailJop
    //{
    //    private readonly INotificationSenderFactory _notificationSenderFactory = notificationSenderFactory;

    //    public async Task<bool> EnqueJobAsync()
    //    {
    //       var emailSender = _notificationSenderFactory.Create(
    //            NotificationChannel.Email,
    //            NotificationMessageType.ConfirmationEmail);

    //        BackgroundJob.Enqueue(async () => await emailSender.SendAsync());
    //    }
    //}
}
