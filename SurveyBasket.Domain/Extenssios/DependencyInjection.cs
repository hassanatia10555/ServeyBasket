using Microsoft.Extensions.Options;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Domain.Options.EmailSettingOptions;
using SurveyBasket.Domain.Options.JwtOptions;

namespace SurveyBasket.Domain.Extenssios;
public static class DependencyInjection
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddServiceContracts<GlobalExeptionHandler>();
        services.AddOptionsConfig();
    }    
    private static IServiceCollection AddServiceContracts<T>(this IServiceCollection services) where T : IExceptionHandler
    {
        services.AddExceptionHandler<GlobalExeptionHandler>();
        services.AddProblemDetails();
        return services;
    }

    private static IServiceCollection AddOptionsConfig(
         this IServiceCollection services)
    {
        // Register the JwtOptionsValidator and JwtOptionsValidation classes
        services.AddSingleton<IValidator<JwtOptions>, JwtOptionsValidator>();

        services.AddSingleton<
            IValidateOptions<JwtOptions>, JwtOptionsValidation>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateOnStart();

        // Register the Email Setting Configuration
        services.AddOptions<EmailSetting>()
            .BindConfiguration(EmailSetting.SectionName)
            .ValidateOnStart();

        return services;
    }
}