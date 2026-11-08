namespace SurveyBasket.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
                            : IdentityDbContext<ApplicationUser>(dbContextOptions)
                            , IApplicationDbContext
    {

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteAnswer> VoteAnswers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                         .Where(entityType => typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)))
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Auth");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Auth");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Auth");            
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Auth");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Auth");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Auth");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Auth");
        }
    }
}
