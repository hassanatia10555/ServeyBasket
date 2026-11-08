namespace SurveyBasket.Infrastructure.Configurations
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions", "dbo");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new {x.PollId , x.Content} )
                .IsUnique();

            builder.Property(x => x.Content)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasOne(x => x.Poll)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.PollId);
        }
    }
}
