using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration )
        {

            services.AddApplicationDbContext(configuration);
        }
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services , IConfiguration configuration )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
