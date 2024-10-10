using Handmade.Models;
using Microsoft.EntityFrameworkCore;

namespace Handmades.Models
{
    public class DataDbContext   : DbContext
    {
        public DataDbContext()
        {
        }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Signup> Signups { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\;Database=Handmade;Trusted_Connection=true; Encrypt=false;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // إعدادات العلاقة بين Product و Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Category_ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.User_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // إعدادات العلاقة بين Order و User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.User_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // تحديد نوع الأعمدة للخصائص من نوع decimal
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // إعدادات العلاقة بين OrderDetails و Order
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.Order_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // تحديد نوع الأعمدة للخصائص من نوع decimal
            modelBuilder.Entity<OrderDetails>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.Product_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // إعدادات العلاقة بين Review و User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.User_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // تحديد نوع الأعمدة للخصائص من نوع decimal
            modelBuilder.Entity<Review>()
                .Property(r => r.Rate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.Product_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // إعدادات العلاقة بين Cart و Product
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.Product_ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Order)
                .WithMany()
                .HasForeignKey(c => c.Order_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // إعدادات العلاقة بين Payment و Order
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.Order_ID)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }

}
