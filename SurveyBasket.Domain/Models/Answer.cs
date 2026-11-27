namespace SurveyBasket.Domain.Models
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public string QuestionId { get; set; } = default!;
        public Question Question { get; set; } = default!;

        public ICollection<VoteAnswer> VoteAnswers { get; set; } = new List<VoteAnswer>();
    }
}
