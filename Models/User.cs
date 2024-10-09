namespace Handmades.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string SignUp { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}
