using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyBasket.Infrastructure.Services.Email
{
    public class EmailBodyBuilderFactory(IServiceProvider serviceProvider) : IEmailBodyBuilderFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public IEmailBodyBuilder Create(string templateName)
        {
            return _serviceProvider.GetRequiredKeyedService<IEmailBodyBuilder>(
                templateName);
        }
    }
}
