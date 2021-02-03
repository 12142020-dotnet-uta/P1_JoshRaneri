using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class OrderInventory
    {
        public OrderInventory()
        {
        }
        [ForeignKey("OrderId")]
        [Required]
        [DisplayName("Order Id")]
        public Guid OrderId { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
        [DisplayName("Order Quantity")]
        public int OrderQuantity { get; set; }
    }
}
