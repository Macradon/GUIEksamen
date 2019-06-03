using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeepNetModelBureau.Models
{
    [Serializable]
    public class Bill
    {
        public long BillId { get; set; }
        [Required]
        [DisplayName("NameOfTask")]
        public string NameOfTask { get; set; }
        [Required]
        [DisplayName("Date")]
        public int Date { get; set; }
        [Required]
        [DisplayName("Text")]
        public string Text { get; set; }
        [Required]
        [DisplayName("Price")]
        public int Price { get; set; }
    }
}
