using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class LocationInventory
    {
        public LocationInventory()
        {
        }
        public LocationInventory(int locId, int prodId, int quant)
        {
            this.LocationId = locId;
            this.ProductId = prodId;
            this.Quantity = quant;
        }
        [ForeignKey("LocationId")]
        [DisplayName("Location Id")]
        public int LocationId { get; set; }
        [ForeignKey("ProductId")]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
    }
}
