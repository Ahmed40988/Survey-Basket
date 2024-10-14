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
    }
}
