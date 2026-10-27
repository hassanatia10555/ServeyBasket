using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Security.Claims;

namespace Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
                                     : IdentityDbContext<ApplicationUser>(dbContextOptions)                                     
    {
        public DbSet<Poll> Polls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
