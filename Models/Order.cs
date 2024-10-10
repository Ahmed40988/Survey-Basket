using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Order
    {
        public int ID { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }


        public int User_ID { get; set; }


        public User User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Payment Payment { get; set; }
        public Cart Cart { get; set; }
    }

}
