using Handmades.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handmade.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DataDbContext _context;

        public DashboardController(DataDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult listproducts()
        {
            var productlist=_context.Products.ToList();
            return View(productlist);
        }
        public IActionResult listusers()
        {
            var listusers = _context.Users.ToList();
            return View(listusers);
        }

        public IActionResult listcategore()
        {
            var listcategore = _context.Categories.ToList();
            return View(listcategore);
        } 
        public IActionResult listorder()
        {
            var listorder = _context.Orders.ToList();
            return View(listorder);
        }

      
    }
}
