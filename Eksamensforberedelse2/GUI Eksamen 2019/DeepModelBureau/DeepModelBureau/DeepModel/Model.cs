using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepModelBureau
{
    [Serializable]
    public class Model
    {
        public string Name { get; set; }
        public int PhoneNo { get; set; }
        public string Address { get; set; }
        public int ModelHeight { get; set; }
        public int ModelWeight { get; set;  }
        public string HairColor { get; set; }
        public string Comments { get; set; }

        public Model()
        {

        }

        public Model(string name, int phoneNo, string adr, int height, int weight, string hairColor, string comments)
        {
            Name = name;
            PhoneNo = phoneNo;
            Address = adr;
            ModelHeight = height;
            ModelWeight = weight;
            HairColor = hairColor;
            Comments = comments; 
        }
    }
}
