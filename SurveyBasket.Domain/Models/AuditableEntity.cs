namespace SurveyBasket.Domain.Models
{
    public class AuditableEntity : BaseEntity
    {
        public string CreatedById { get; set; } = default!;
        public DateTime CreatedOn = DateTime.UtcNow;
        public string? UpdatedById{ get; set; }
        public DateTime? UpdatedOn{ get; set; }

        public ApplicationUser CreatedBy = default!;
        public ApplicationUser? UpdatedBy;
    }
}
