using Core.Authentication;
using Core.Contracts.JwtOptions;
using Core.Contracts.Poll;
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
        services.AddJwtConfig();
    }

    private static IServiceCollection AddValidationServices(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PollRequestValidator>();

        services.AddSingleton<IValidator<JwtOptions>, JwtOptionsValidator>();

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
        services.AddSingleton<
            IValidateOptions<JwtOptions>,JwtOptionsValidation>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateOnStart();

        return services;
    }

    private static IServiceCollection AddJwtConfig(
        this IServiceCollection services)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        services.AddOptions<JwtBearerOptions>(
                JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtOptions>>(
                (options, jwtOptionsAccessor) =>
                {
                    var jwtOptions = jwtOptionsAccessor.Value;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtOptions.Key)),

                            ValidateIssuer = true,
                            ValidIssuer = jwtOptions.Issuer,

                            ValidateAudience = true,
                            ValidAudience = jwtOptions.Audience,

                            ValidateLifetime = true
                        };
                });

        return services;
    }
}