using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cp_11.Logic.model;

namespace cp_11.Logic.model
{
    public class Ticket : IEquatable<Ticket>
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

        public bool IsTicketBought(Ticket newTicket)
        {
            if (Seat == newTicket.Seat && Cab == newTicket.Cab)
            { return true; }
            return false;
        }
    }
    
   public interface IEquatable<T>
    {
        bool IsTicketBought(T newTicket);
    }
}
