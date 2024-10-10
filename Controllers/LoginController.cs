using Handmades.Models;
using Handmade.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Handmade.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataDbContext _context;

        public LoginController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Show login page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle login form submission
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Query the database to find a user with the provided email and password
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Login successful: Redirect to the main page (or wherever you want)
                return RedirectToAction("Index");
            }

            // If login fails, display an error message
            ViewBag.ErrorMessage = "Invalid email or password. Please try again.";
            return View();
        }

        // Main page after login
        public IActionResult Index()
        {
            return View();
        }
    }
}
