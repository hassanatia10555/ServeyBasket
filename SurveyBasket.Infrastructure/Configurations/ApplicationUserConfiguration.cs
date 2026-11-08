namespace SurveyBasket.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .OwnsMany(x => x.RefreshTokens, rt =>
                {
                    rt.ToTable("RefreshTokens" , "Auth");

                    rt.WithOwner()
                      .HasForeignKey("UserId");

                    rt.Property(rt => rt.Token)
                      .IsRequired()
                      .HasMaxLength(500);

                    rt.Property(x => x.CreatedAt)
                      .IsRequired();

                    rt.Property(x => x.RevokedOn);                     
                });               
        }
    }
}
