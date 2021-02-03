using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CartInventory
    {
        public CartInventory()
        {
        }
        [ForeignKey("CartId")]
        [Required]
        [DisplayName("Cart Id")]
        public Guid CartId { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
        [DisplayName("Cart Quantity")]
        public int CartQuantity { get; set; }
    }
}
