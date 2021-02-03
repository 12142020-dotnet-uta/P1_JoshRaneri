using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Product
    {
        public Product()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
