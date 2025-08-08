namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        //Default Data

        builder.HasData([
            new ApplicationRole
            {
               Id=DefaultRoles.AdminRoleId,
               Name=DefaultRoles.Admin,
               NormalizedName=DefaultRoles.Admin.ToLower(),
               ConcurrencyStamp=DefaultRoles.AdminRoleConcurrencyStamp
            },
            new ApplicationRole
            {
               Id=DefaultRoles.MemberRoleId,
               Name=DefaultRoles.Member,
               NormalizedName=DefaultRoles.Member.ToLower(),
               ConcurrencyStamp=DefaultRoles.MemberRoleConcurrencyStamp,
               IsDefault=true
            }
            ]);
    }
}