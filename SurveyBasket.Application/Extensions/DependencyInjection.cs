namespace SurveyBasket.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddServiceContracts();
            services.AddMappingServices();
        }
        public static IServiceCollection AddServiceContracts(this IServiceCollection services)
        {
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
        private static IServiceCollection AddMappingServices(
                this IServiceCollection services)
        {
            var mapperConfig = TypeAdapterConfig.GlobalSettings;

            mapperConfig.Scan(typeof(DependencyInjection).Assembly);

            services.AddSingleton(mapperConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }        
    }
}
