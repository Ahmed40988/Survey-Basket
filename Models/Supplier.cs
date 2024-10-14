using Handmades.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Handmades.Models
{
    public class Supplier
    {

        [Key]
        public int SupplierId { get; set; }
       //foreignkey for user
        public int? ID { get; set; }

        public int? RoleId { get; set; }

        [InverseProperty("Supplier")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [ForeignKey("RoleId")]

        public virtual Role? Role { get; set; }

        [ForeignKey("ID")]

        public virtual User? User { get; set; }
    }
}
