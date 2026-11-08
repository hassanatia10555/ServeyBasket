namespace SurveyBasket.Domain.Models
{
    public class Poll : AuditableEntity
    {
        public string Title { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public bool IsPublished { get; set; } = false;
        public DateTime? StartsAt { get; set; } 
        public DateTime? EndsAt { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
