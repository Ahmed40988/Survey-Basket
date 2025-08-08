
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SurveyBasket.Api.Persistence
{
    public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options, IHttpContextAccessor httpContextAccessor) :
        IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Poll> polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteAnswer> VoteAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            var cascadeFKs = modelBuilder.Model
        .GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            foreach (var entry in entries)
            {
                var userid = _httpContextAccessor.HttpContext?.User.GetUserId();
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedByid).CurrentValue = userid!;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedByid).CurrentValue = userid!;
                    entry.Property(x => x.Updatedon).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }

}
