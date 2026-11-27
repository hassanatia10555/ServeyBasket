using Hangfire;
using SurveyBasket.Application.Contracts.TemplateModels;
using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;
using SurveyBasket.Infrastructure.Services.EmailService;
using SurveyBasket.Infrastructure.Services.EmailService.Abstractions.Email;
using SurveyBasket.Infrastructure.Services.EmailService.ConfirmationEmail;
using SurveyBasket.Infrastructure.Services.NotificationService.Abstractions;

namespace SurveyBasket.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddApplicationDbContext(configuration);
            services.AddIdentityServices();
            services.AddJwtConfig();
            services.AddServicesLifeTime();
            services.AddHangfireServices(configuration);
        }
        private static IServiceCollection AddServicesLifeTime(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<INotificationSenderFactory, NotificationSenderFactory>();
            services.AddScoped<IMessageBodyBuilderFactory, MessageBodyBuilderFactory>();

            services.AddKeyedTransient
                <INotificationSender, EmailNotificationSender>(NotificationChannel.Email);

            services.AddKeyedTransient<
                IMessageBodyBuilder<ConfirmationEmailTemplateModel>,
                ConfirmationEmailMessageBodyBuilder>(
                (
                    NotificationChannel.Email,
                    NotificationMessageType.ConfirmationEmail,
                    typeof(ConfirmationEmailTemplateModel)
                ));

            services.AddKeyedTransient<
                IMessageBodyBuilder<NewPollEmailTemplateModel>,
                NewPollEmailMessageBodyBuilder>(
                (
                    NotificationChannel.Email,
                    NotificationMessageType.NewPoll,
                    typeof(NewPollEmailTemplateModel)
                ));

            services.AddScoped<AuditInterceptor>();
            services.AddHttpContextAccessor();
            return services;
        }
        private static IServiceCollection AddApplicationDbContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));

                options.AddInterceptors(
                    sp.GetRequiredService<AuditInterceptor>());
            });

            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

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

        private static IServiceCollection AddHangfireServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(
                    configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();

            return services;
        }

    }
}
