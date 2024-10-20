using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Cart
    {
        public int ID { get; set; } // Primary Key
        public int Product_ID { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }

}

