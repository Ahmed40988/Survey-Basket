
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Enframework_learning_2
{
	public class DbcontextEfcoreold : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=EfcoreLearning;Integrated Security=True;Encrypt=False;TrustServerCertificate=True");
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Post { get; set; }
		public DbSet<Blogimage> blogimage { get; set; }
		public DbSet<Stock> stocks { get; set; }
		//public DbSet<Post>posts {  get; set; }
		//public DbSet<category>categories {  get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Blog>()
					.HasOne(b => b.Blogimage)
					.WithOne(i => i.blog)
					.HasForeignKey<Blogimage>(f => f.Blogforignkey);


			modelBuilder.Entity<Blogimage>()
				.HasMany(p=>p.Posts)
				.WithOne(p=>p.blogimage)
				.HasForeignKey(f => f.Blogimageid);

			modelBuilder.Entity<Blog>()
				.HasMany(p => p.Posts)
				.WithOne(p => p.Blog)
				.HasForeignKey(p => new { p.Blogid });
				


			//modelBuilder.Entity<Blog>()
			//	.HasData(new Blog { Id = 2, url = "jsdnkcl.come" });








		}

	}
}
