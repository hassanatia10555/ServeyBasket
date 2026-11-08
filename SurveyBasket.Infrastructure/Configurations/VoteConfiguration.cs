namespace SurveyBasket.Infrastructure.Configurations
{
    internal class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Votes", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.SubmittedOn)
                .IsRequired();

            builder.HasOne(x => x.Poll)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.PollId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
