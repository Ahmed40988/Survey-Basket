using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Review
    {
        public int ID { get; set; } // Primary Key
        public decimal Rate { get; set; }
        public string Text { get; set; }

        // Foreign Keys
        public int User_ID { get; set; }
        public int Product_ID { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public User User { get; set; }
    }

}
