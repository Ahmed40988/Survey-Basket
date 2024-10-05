using Microsoft.AspNetCore.Mvc;
using Handmades.Models;
using Handmade.Models;

namespace Handmade.Controllers
{
    public class SignupController : Controller
    {
        private readonly DataDbContext _context;

        public SignupController(DataDbContext context)
        {
            _context = context;
        }

        public IActionResult Signup()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult ADDnewuser(Signup signup)
        {
            if (ModelState.IsValid) // تأكد من صحة النموذج
            {
                _context.Signups.Add(signup); // تأكد من استخدام الاسم الصحيح
                _context.SaveChanges();
                return RedirectToAction("Sucssfulsignup");
            }
            return View("Signup", signup);
        }

        public IActionResult Sucssfulsignup()
        {
            return View();
        }
    }
}
