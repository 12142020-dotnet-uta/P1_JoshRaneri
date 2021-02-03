using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CustomUser : IdentityUser
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        [ForeignKey("LocationId")]
        [DisplayName("Location Id")]
        public int DefaultStore { get; set; }
        [ForeignKey("CartId")]
        [DisplayName("Cart Id")]
        public Guid CartId { get; set; }
    }
}
