using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ModelLayer.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Default Location")]
        public Location DefaultStore { get; set; }
    }
}
