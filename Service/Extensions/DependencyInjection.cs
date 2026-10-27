using Core.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
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
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
