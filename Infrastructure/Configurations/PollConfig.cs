namespace Infrastructure.EntityConfigurations
{
    internal class PollConfig : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Summary)
                .IsRequired()
                .HasMaxLength(500);   
            
            builder.Property(x => x.IsPublished)
                .IsRequired();

            builder.Property(x => x.StartsAt)
                .IsRequired(false);

            builder.Property(x => x.EndsAt)
                .IsRequired(false);
        }        
    }
}
