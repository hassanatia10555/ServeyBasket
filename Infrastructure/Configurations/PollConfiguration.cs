using Infrastructure.Configurations;

namespace Infrastructure.EntityConfigurations
{

    internal class PollConfiguration : AuditableEntityConfiguration<Poll>
    {
        public override void Configure(
            EntityTypeBuilder<Poll> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Summary)
                .HasMaxLength(500);

            builder.Property(x => x.IsPublished)
                .IsRequired();

            builder.Property(x => x.StartsAt)
                .IsRequired();
            builder.Property(x => x.EndsAt)
                .IsRequired();
        }
    }
}

