namespace Core.Extensions
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddValidationServices();
            services.AddMappingServices();
        }
        public static void AddCoreContracts(this IServiceCollection services)
        {
            // Register core contracts here
        }
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<PollRequestValidator>();
            services.AddFluentValidationAutoValidation();

            return services;
        }
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(typeof(DependencyInjection).Assembly);

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
