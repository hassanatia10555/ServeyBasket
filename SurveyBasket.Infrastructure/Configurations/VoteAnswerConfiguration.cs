namespace SurveyBasket.Infrastructure.Configurations
{
    internal class VoteAnswerConfiguration : IEntityTypeConfiguration<VoteAnswer>
    {
        public void Configure(EntityTypeBuilder<VoteAnswer> builder)
        {
            builder.ToTable("VoteAnswers", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Vote)
                .WithMany(x => x.VoteAnswers)
                .HasForeignKey(x => x.VoteId);

            builder.HasOne(x => x.Question)
                .WithMany(x => x.VoteAnswers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Answer)
                .WithMany(x => x.VoteAnswers)
                .HasForeignKey(x => x.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
