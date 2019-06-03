using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GUIEksamen2019SmartCityWeb.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationID { get; set; }
        [Required]
        [DisplayName("Location Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Location Address")]
        public string Address { get; set; }
        [Required]
        [DisplayName("List of Trees at Location")]
        public string ListOfTrees { get; set; }
    }
}
