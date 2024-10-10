using Microsoft.EntityFrameworkCore;
namespace Handmades.Models
{
    public class Account
    {
        public int ID { get; set; } // Primary Key
        public string Login { get; set; }
        public string SignUp { get; set; }
    }

}
