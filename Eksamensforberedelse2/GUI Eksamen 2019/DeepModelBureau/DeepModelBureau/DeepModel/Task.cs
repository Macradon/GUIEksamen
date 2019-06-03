using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepModelBureau
{
    [Serializable]
    public class Task
    {
        public string CosName { get; set; }
        public int Date { get; set; }
        public int NoOfDays { get; set; }
        public string Location { get; set; }
        public int NoOfModels { get; set; }
        public string Comments { get; set; }

        public Task()
        {

        }

        public Task(string name, int date, int noOfDays, string location, int noOfModels, string comments)
        {
            CosName = name;
            Date = date;
            NoOfDays = noOfDays;
            Location = location;
            NoOfModels = noOfModels;
            Comments = comments;
        }
    }
}
