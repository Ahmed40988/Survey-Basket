using Handmade.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Handmades.Models
{
    public class User
    {
        public int ID { get; set; }  // Primary Key
        public string Name { get; set; }  // اسم المستخدم
        public string Email { get; set; }  // البريد الإلكتروني
        public string imageUrl { get; set; }
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
        // Navigation properties
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}
