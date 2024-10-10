using Microsoft.AspNetCore.Mvc;
using Handmades.Models;
using Handmade.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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

        //[HttpPost]
        public async Task<IActionResult> ADDnewuser(Signup signup, IFormFile imageurl)
        {
            if (signup.Name != null) // التحقق من وجود اسم المستخدم
            {
                if (imageurl != null && imageurl.Length > 0)
                {
                    // تحديد المسار للصورة
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageurl.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageurl.CopyToAsync(stream);
                    }

                    // تخزين المسار في الخاصية imageurl داخل النموذج
                    signup.imageurl = "/images/" + imageurl.FileName;
                }

                // إضافة النموذج إلى قاعدة البيانات في جدول Signup
                _context.Signups.Add(signup);

                // إنشاء كائن User جديد وحفظه في جدول Users
                var user = new User
                {
                    Name = signup.Name,
                    Email = signup.Email,
                    imageUrl = signup.imageurl // تأكد من استخدام الاسم الصحيح للخاصية
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // حفظ التغييرات في قاعدة البيانات

                return RedirectToAction("Sucssfulsignup", new { id = user.ID }); // استخدم ID المستخدم الجديد
            }

            return View("Signup", signup); // في حالة فشل النموذج، قم بإعادة عرض صفحة التسجيل
        }
        public IActionResult Sucssfulsignup(int id)
        {
            // احصل على المستخدم من قاعدة البيانات باستخدام الـ ID
            var user = _context.Users.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound(); // في حالة عدم العثور على المستخدم
            }

            return View(user); // تمرير المستخدم إلى العرض
        }

    }

    }
