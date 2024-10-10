using System.Collections.Generic;

namespace Handmades.Models
{
    public class User
    {
        public int ID { get; set; } // Primary Key
        public string Login { get; set; }
        public string SignUp { get; set; }

        // Navigation properties
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string Email { get; internal set; }
        public string Password { get; internal set; }
    }

}
