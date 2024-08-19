using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_11.Logic.model
{
    public class Station
    {
        public string Name { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public string StayTime { get; set; }
        public string Distance { get; set; }
        public bool IsBegin { get; set; }
        public bool IsFinish { get; set; }
    }
}
