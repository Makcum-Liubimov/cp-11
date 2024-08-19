using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_11.Logic.model
{
    public class Train
    {
        public List<Station> Stations { get; set; }
        public string StartStation { get; set; }
        public string FinishStation { get; set; }
        public string NumberOfTrain { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }

    }
}
