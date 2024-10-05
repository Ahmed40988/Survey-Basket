namespace Handmades.Models
{
    public class Order
    {
        public int ID { get; set; } // Primary Key
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign Key
        public int User_ID { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Payment Payment { get; set; }
        public Cart Cart { get; set; }
    }

}
