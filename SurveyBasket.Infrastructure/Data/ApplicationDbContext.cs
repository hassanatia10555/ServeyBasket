namespace SurveyBasket.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
                            : IdentityDbContext<ApplicationUser>(dbContextOptions)
                            , IApplicationDbContext
    {
                            
        public DbSet<Poll> Polls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
