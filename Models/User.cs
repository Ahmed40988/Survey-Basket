using System.Collections.Generic;

namespace Handmades.Models
{
    public class User
    {
        public int ID { get; set; }  // Primary Key

        public string Name { get; set; }  // اسم المستخدم

        public string Email { get; set; }  // البريد الإلكتروني

        public string imageUrl { get; set; }  // كلمة المرور (يُفضل تشفيرها)

        // Navigation properties
        public ICollection<Product> Products { get; set; }  // المنتجات اللي البائع بيعرضها

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
    }

