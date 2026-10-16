using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServiceServices(this IServiceCollection services )
        {
            services.AddServiceContracts();
        }
        public static IServiceCollection AddServiceContracts(this IServiceCollection services)
        {
            services.AddScoped<IPollService, PollService>();
            return services;
        }
    }
}
