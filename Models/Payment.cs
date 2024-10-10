using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Payment
    {
        public int ID { get; set; } // Primary Key
        public string PaymentMethod { get; set; }
        public string Cash { get; set; }
        public string Delivery { get; set; }

        // Foreign Key
        public int Order_ID { get; set; }

        // Navigation properties
        public Order Order { get; set; }
    }

}
