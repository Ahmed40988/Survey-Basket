using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Cart
    {
        public int ID { get; set; } // Primary Key

        // Foreign Keys
        public int Product_ID { get; set; }
        public int Order_ID { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
