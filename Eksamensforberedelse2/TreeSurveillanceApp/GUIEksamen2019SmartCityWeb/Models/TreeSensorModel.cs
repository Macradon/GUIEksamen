using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GUIEksamen2019SmartCityWeb.Models
{
    public class TreeSensorModel
    {
        [Key]
        public string SensorID { get; set; }
        [Required]
        [DisplayName("Sort of Tree")]
        public string TreeSort { get; set; }
        [Required]
        [DisplayName("Location ID")]
        public int LocationID { get; set; }
        [Required]
        [DisplayName("Coordinate Location at")]
        public double CoordinateIat { get; set; }
        [Required]
        [DisplayName("Coordinate Location On")]
        public double CoordinateIon { get; set; }
    }
}