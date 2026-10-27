namespace Core.Models
{
    public class Poll : AuditableEntity
    {
        public string Title { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public bool IsPublished { get; set; } = false;
        public DateTime? StartsAt { get; set; } 
        public DateTime? EndsAt { get; set; }
    }
}
