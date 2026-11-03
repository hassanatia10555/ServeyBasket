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
            services.AddJwtOptionsConfig();
        }
        private static IServiceCollection AddServicesLifeTime(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<IApplicationDbContext , ApplicationDbContext>();

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService,
                CurrentUserService>();

            services.AddScoped<AuditInterceptor>();

            return services;
        }
        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services,IConfiguration configuration)
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
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
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
        private static IServiceCollection AddJwtOptionsConfig(
                 this IServiceCollection services)
        {
            services.AddSingleton<IValidator<JwtOptions>, JwtOptionsValidator>();

            services.AddSingleton<
                IValidateOptions<JwtOptions>, JwtOptionsValidation>();

            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateOnStart();

            return services;
        }
    }
}
