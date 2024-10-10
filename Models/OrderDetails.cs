using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class OrderDetails
    {
        public int ID { get; set; } // Primary Key
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Foreign Keys
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
