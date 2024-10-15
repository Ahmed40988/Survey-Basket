using Handmade.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NuGet.Client.ManagedCodeConventions;

namespace Handmades.Models
{
    public class Signup
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Unique("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Unique("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
       
        public string imageurl  { get; set; }


        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
