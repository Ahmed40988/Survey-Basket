using Handmades.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Handmades.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [StringLength(50)]

        public string? RoleName { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();


        public virtual ICollection<User> Users { get; set; } = new List<User>();

    }

}