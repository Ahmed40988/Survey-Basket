using Microsoft.EntityFrameworkCore;
using Handmades.Models;
using Microsoft.AspNetCore.Http.Features;

namespace Handmade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // إضافة سلسلة الاتصال الخاصة بك
            builder.Services.AddDbContext<DataDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // إضافة إعدادات الحد الأقصى لحجم الملفات
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 ميغابايت
            });

            // إضافة خدمات الجلسة
            builder.Services.AddDistributedMemoryCache(); // يستخدم لتخزين بيانات الجلسات في الذاكرة
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // مدة الجلسة 30 دقيقة
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // إضافة الخدمات إلى الحاوية.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // تكوين قناة طلب HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // تفعيل الجلسات في التطبيق
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
