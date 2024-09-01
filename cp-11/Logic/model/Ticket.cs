using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cp_11.Logic.model;

namespace cp_11.Logic.model
{
    public class Ticket : ITicket, IEquatable<Ticket>
    {

        public double Cost { get; set; }
        public int Seat { get; set; }
        public string Cab { get; set; }
        public string PassengerName { get; set; }
        public string TrainNumber { get; set; }
        public string Destintaion { get; set; }
        public string Source { get; set; }
        public string TimeOfArrival { get; set; }
        public string TimeOfDeparture { get; set; }

        public bool Equals(Ticket? other)
        {
            
            if(PassengerName == other.PassengerName && Seat == other.Seat && TrainNumber == other.TrainNumber) return true;
            return false;
        }
    }
    

    interface ITicket
    {
        double Cost { get; set; }
        int Seat { get; set; }
        string Cab { get; set; }
        string PassengerName { get; set; }
        string TrainNumber { get; set; }
        string Destintaion { get; set; }
        string Source { get; set; }
        string TimeOfArrival { get; set; }
        string TimeOfDeparture { get; set; }
    }

}
   

