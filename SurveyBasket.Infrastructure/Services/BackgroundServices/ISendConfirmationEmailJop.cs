using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Infrastructure.Services.BackgroundServices
{
    internal interface ISendConfirmationEmailJop
    {
        Task<bool> EnqueJobAsync();
    }
}
