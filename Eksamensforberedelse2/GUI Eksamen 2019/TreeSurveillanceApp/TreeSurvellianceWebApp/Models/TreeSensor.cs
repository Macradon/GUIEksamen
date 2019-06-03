using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TreeSurvellianceWebApp.Models
{
    public class TreeSensor
    {
        [Required]
        [DisplayName("LocationID")]
        public Int16 LocationID { get; set; }
        [Required]
        [DisplayName("TreeSort")]
        public string TreeSort { get; set; }
        [Required]
        [DisplayName("SensorID")]
        public string SensorID { get; set; }
        [Required]
        [DisplayName("Coordinates")]
        public List<double> Coordinates { get; set; }
    }
}
