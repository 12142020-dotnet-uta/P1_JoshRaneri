using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Order
    {
        public Order()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Order Id")]
        public Guid OrderId { get; set; } = Guid.NewGuid();
        [ForeignKey("LocationId")]
        [DisplayName("Location Id")]
        public int LocationId { get; set; }
        [ForeignKey("Id")]
        [DisplayName("User Id")]
        public string Id { get; set; }
        [ForeignKey("CartId")]
        [DisplayName("Cart Id")]
        public Guid CartId { get; set; }
        [Required]
        [DisplayName("Order Date")]
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Order Total")]
        public decimal OrderTotal { get; set; }
    }
}
