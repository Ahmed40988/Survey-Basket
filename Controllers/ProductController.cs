using Microsoft.AspNetCore.Mvc;
using Handmades.Models;
using Handmade.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Handmade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;


namespace Handmade.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataDbContext _context;

        public ProductController(DataDbContext context)
        {
            _context = context;
        }
        public IActionResult Add()
        {
            var categories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.Categories = new SelectList(categories, "Value", "Text");
            return View();
        }
        public async Task<IActionResult> Addnewproduct(Product product, IFormFile ImageUrl)
        {
            if (product.Name != null)
                if (product.Name != null)
                {
                    if (ImageUrl != null && ImageUrl.Length > 0)
                    {
                        // تحديد المسار للصورة
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImageUrl.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await ImageUrl.CopyToAsync(stream);
                        }

                        // تخزين المسار في الخاصية imageurl داخل النموذج
                        product.ImageUrl = "/images/" + ImageUrl.FileName;
                    }

                    // Set the User_ID to the logged-in user's ID
                   // product.User_ID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }

            // Repopulate categories if model state is invalid
            var categories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.Categories = new SelectList(categories, "Value", "Text");
            return View("Add", product);
        }
      
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.FirstOrDefault(d => d.ID == id);
            return View("Edit", product);
        }
        public async Task<IActionResult> saveaEditdep(int id, Product product, IFormFile ImageUrl)
        {
            if (product.Name != null)
            {
                Product productDb = _context.Products.FirstOrDefault(d => d.ID == id);

                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    // تحديد المسار للصورة الجديدة
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImageUrl.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }

                    // تحديث مسار الصورة في النموذج
                    productDb.ImageUrl = "/images/" + ImageUrl.FileName;
                }
                else
                {
                    // إذا لم يتم رفع صورة جديدة، احتفظ بالصورة القديمة
                    productDb.ImageUrl = productDb.ImageUrl;
                }

                // تحديث باقي الخصائص
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Address = product.Address;
                productDb.User_ID = product.User_ID;
                productDb.Category_ID = product.Category_ID;
                productDb.Price = product.Price;
                productDb.SupplierId = product.SupplierId;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View("Edit", product);
        }

        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(d => d.ID == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("listproducts", "Dashboard");
        }


    }
}
