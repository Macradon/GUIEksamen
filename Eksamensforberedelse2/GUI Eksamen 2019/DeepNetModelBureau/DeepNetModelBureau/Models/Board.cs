using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeepNetModelBureau.Models
{
    [Serializable]
    public class Board
    {
        public long BoardId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Date")]
        public int Date { get; set; }
        [Required]
        [DisplayName("NoOfDays")]
        public int NoOfDays { get; set; }
        [Required]
        [DisplayName("Location")]
        public string Location { get; set; }
        [Required]
        [DisplayName("NoOfModels")]
        public int NoOfModels { get; set; }
        [Required]
        [DisplayName("Comments")]
        public string Comments { get; set; }
    }
}