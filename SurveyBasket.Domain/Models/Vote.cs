namespace SurveyBasket.Domain.Models
{
    public class Vote : BaseEntity
    {
        public Guid PollId { get; set; }
        public Poll Poll { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;

        public ICollection<VoteAnswer> VoteAnswers { get; set; } = new List<VoteAnswer>();
    }
}
