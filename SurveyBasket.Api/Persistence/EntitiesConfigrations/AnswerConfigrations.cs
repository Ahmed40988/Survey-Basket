
namespace SurveyBasket.Api.Persistence.EntitiesConfigrations
{
    public class AnswerConfigration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(q => new { q.QuestionId, q.Content }).IsUnique();
            builder.Property(q => q.Content).HasMaxLength(1000);
        }
    }
}
