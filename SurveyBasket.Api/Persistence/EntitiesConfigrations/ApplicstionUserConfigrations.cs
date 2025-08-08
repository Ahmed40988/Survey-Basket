
namespace SurveyBasket.Api.Persistence.EntitiesConfigrations
{
    public class ApplicstionUserConfigrations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.Fname).HasMaxLength(150);
            builder.Property(x => x.Lname).HasMaxLength(150);

            builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");
        }
    }
}
