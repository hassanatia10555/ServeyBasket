namespace SurveyBasket.Domain.Models
{
    public class Question : BaseEntity
    {
        public string Content { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public Guid PollId { get; set; }
        public Poll Poll { get; set; } = default!;

        public ICollection<Answer> Answers { get; set; } = [];
        public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];
    }
}
