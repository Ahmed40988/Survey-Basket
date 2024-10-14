using Handmades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Handmade.Controllers
{
    public class UserController : Controller
    {
        private readonly DataDbContext context;

        public UserController(DataDbContext context)
        {
            this.context = context; // استخدم Dependency Injection
        }

        public IActionResult Index()
        {
            var users = context.Users.ToList(); // احصل على جميع المستخدمين
            return View(users); // مرر المستخدمين إلى العرض
        }

        public IActionResult Showuser(int id)
        {
            User D1 = context.Users.FirstOrDefault(d => d.ID == id);
            if (D1 == null) // تحقق من وجود المستخدم
            {
                return NotFound(); // إذا لم يكن موجودًا، ارجع خطأ 404
            }
            return View("Showuser", D1); // مرر المستخدم إلى العرض
        }

        public IActionResult Edit(int id)
        {
            User user = context.Users.FirstOrDefault(d => d.ID == id);
            return View("Edit", user);
        }
        public async Task<IActionResult> saveaEditdep(int id, User user, IFormFile ImageUrl)
        {
            if (user.Name != null)
            {
                User userDb = context.Users.FirstOrDefault(d => d.ID == id);

                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    // تحديد المسار للصورة الجديدة
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImageUrl.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }

                    // تحديث مسار الصورة في النموذج
                    userDb.imageUrl = "/images/" + ImageUrl.FileName;
                }
                else
                {
                    // إذا لم يتم رفع صورة جديدة، احتفظ بالصورة القديمة
                    userDb.imageUrl = userDb.imageUrl;
                }

                // تحديث باقي الخصائص
                userDb.Name = user.Name;
                userDb.Email = user.Email;
                userDb.imageUrl = user.imageUrl;
                userDb.RoleId = user.RoleId;

                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View("Edit", user);
        }




        public IActionResult Delete(int id)
        {
            User user = context.Users.FirstOrDefault(d => d.ID == id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("listusers", "Dashboard");
        }
    }
}
