using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Location
    {
        public Location()
        {
        }
        public Location(string locName)
        {
            this.LocationName = locName;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Location Id")]
        public int LocationId { get; set; }
        //[Required]
        [DisplayName("Location Name")]
        public string LocationName { get; set; }
    }
}
