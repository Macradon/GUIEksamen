using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepModelBureau
{
    [Serializable]
    public class Assignment
    {
        public Assignment(string modelName, string taskName)
        {
            ModelName = modelName;
            TaskName = taskName;
        }

        public string ModelName { get; set; }
        public string TaskName { get; set; }
    }
}
