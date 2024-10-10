using Handmades.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
