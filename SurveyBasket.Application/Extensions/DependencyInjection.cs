using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace SurveyBasket.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddServiceContracts();
            services.AddMappingServices();
            services.AddValidationService();
        }
        private static IServiceCollection AddServiceContracts(this IServiceCollection services)
        {
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddExceptionHandler<GlobalExeptionHandler>();
            services.AddProblemDetails();

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
