using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Cart
    {
        public Cart()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Cart Id")]
        public Guid CartId { get; set; } = Guid.NewGuid();
        [ForeignKey("Id")]
        [DisplayName("User Id")]
        public string Id { get; set; }
        [ForeignKey("LocationId")]
        [DisplayName("Location Id")]
        public int LocationId { get; set; }
    }
}
