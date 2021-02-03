using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class OrderInvoiceViewModel
    {
        [DisplayName("User Id")]
        public string Id { get; set; }
        [DisplayName("Order Id")]
        public Guid OrderId { get; set; }
        [DisplayName("Location Id")]
        public int LocationId { get; set; }
        [DisplayName("Product")]
        public Product Product { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Line Total")]
        public decimal LineTotal { get; set; }
    }
}
