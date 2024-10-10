using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Login
    {
        public int ID { get; set; } // Primary Key
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
