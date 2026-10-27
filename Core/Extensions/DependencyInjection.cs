using Core.Contracts.JwtOptions;
using Core.Contracts.Poll;
using Core.Interfaces.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class DependencyInjection
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddValidationServices();
        services.AddMappingServices();
        services.AddJwtOptionsConfig();
    }

    private static IServiceCollection AddValidationServices(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PollRequestValidator>();

        services.AddFluentValidationAutoValidation();

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

    private static IServiceCollection AddJwtOptionsConfig(
        this IServiceCollection services)
    {
        services.AddSingleton<IValidator<JwtOptions>, JwtOptionsValidator>();

        services.AddSingleton<
            IValidateOptions<JwtOptions>,JwtOptionsValidation>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateOnStart();

        return services;
    }    
}