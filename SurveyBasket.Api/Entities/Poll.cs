
namespace SurveyBasket.Api.Entities
{
    public sealed class Poll : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public bool IsPubliched { get; set; } = false;
        public DateOnly Startsat { get; set; }
        public DateOnly Endsat { get; set; }
        public ICollection<Question> questions { get; set; } = [];
        public ICollection<Vote> Votes { get; set; } = [];


    }
}
