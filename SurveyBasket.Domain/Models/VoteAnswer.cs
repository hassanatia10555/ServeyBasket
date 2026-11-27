namespace SurveyBasket.Domain.Models
{
    public class VoteAnswer : BaseEntity
    {
        public string VoteId { get; set; } = default!;
        public Vote Vote { get; set; } = default!;

        public string QuestionId { get; set; } = default!;
        public Question Question { get; set; } = default!;

        public string AnswerId { get; set; } = default!;
        public Answer Answer { get; set; } = default!;
    }
}
