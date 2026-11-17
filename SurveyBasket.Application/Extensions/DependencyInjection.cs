using SurveyBasket.Domain.Extenssios;

namespace SurveyBasket.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddServiceContracts();
            services.AddMappingServices();
            services.AddValidationService();
            services.AddDomainServices();
        }
        private static IServiceCollection AddServiceContracts(this IServiceCollection services)
        {
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IResultsService, ResultsService>();

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
        private static IServiceCollection AddValidationService(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<QuestionRequestValidator>();
            return services;
        }
    }
}
