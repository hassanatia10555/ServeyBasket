using SurveyBasket.Infrastructure.Services.Email;
using SurveyBasket.Infrastructure.Services.Email.ConfirmationEmail;

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
        }
        private static IServiceCollection AddServicesLifeTime(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddTransient<INotificationService, ConfirmEmailNotificationService>();

            services.AddKeyedTransient<IEmailBodyBuilder,
                        ConfirmationEmailBodyBuilder>("ConfirmationEmail");

            services.AddTransient<IEmailBodyBuilderFactory,EmailBodyBuilderFactory>();

            services.AddScoped<AuditInterceptor>();
            services.AddHttpContextAccessor();
            return services;
        }
        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
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
    }
}
