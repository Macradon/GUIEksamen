using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIEksamen2019SmartCity.Models
{
    public class Location
    {
        public int ID { get; set;}
        public string Name { get; set; }
        public string Street { get; set; }
        public int StreetNum { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string TreeMonitorList { get; set; }

        public Location()
        {

        }

        public Location(string name, 
                        string street, 
                        int streetNum, 
                        int zipCode, 
                        string city, 
                        string treeMonitorList)
        {
            Name = name;
            Street = street;
            StreetNum = streetNum;
            ZipCode = zipCode;
            City = city;
            TreeMonitorList = treeMonitorList;
        }
    }
}
