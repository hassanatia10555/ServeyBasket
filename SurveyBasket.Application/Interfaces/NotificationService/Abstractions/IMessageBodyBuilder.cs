using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Application.Interfaces.NotificationService.Abstractions
{
    public interface IMessageBodyBuilder<TModel>
    {
        public string Build(TModel model);
    }
}
