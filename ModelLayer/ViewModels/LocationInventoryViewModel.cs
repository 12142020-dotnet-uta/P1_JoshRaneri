using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    //[BindProperties(SupportsGet = true)]
    public class LocationInventoryViewModel
    {
        [DisplayName("Location")]
        public Location Location { get; set; }
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
        [DisplayName("Product Description")]
        public string Description { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Quantity On Hand")]
        public int Quantity { get; set; }
        [DisplayName("Purchase Quantity")]
        [Range(0, 99)]
        public int purchaseQuantity { get; set; }
    }

}
