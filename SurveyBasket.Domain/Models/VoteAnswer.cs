namespace SurveyBasket.Domain.Models
{
    public class VoteAnswer : BaseEntity
    {
        public Guid VoteId { get; set; }
        public Vote Vote { get; set; } = default!;

        public Guid QuestionId { get; set; }
        public Question Question { get; set; } = default!;

        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; } = default!;
    }
}
