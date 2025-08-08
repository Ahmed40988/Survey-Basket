namespace SurveyBasket.Api.Entities
{
    public class AuditableEntity
    {
        public string CreatedByid { get; set; } = string.Empty;
        public DateTime Createdon { get; set; } = DateTime.UtcNow;
        public string? UpdatedByid { get; set; }
        public DateTime? Updatedon { get; set; }
        public ApplicationUser CreatedBy { get; set; } = default!;

        public ApplicationUser? UpdatedBy { get; set; }


    }
}
