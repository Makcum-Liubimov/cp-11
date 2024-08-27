using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cp_11.Logic.model;

namespace cp_11.Logic.model
{
    public class Ticket
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

       
    }
    
    public interface ITicket
    {
        CabType Type { get; set; }
        void changeTicket(CabType Newtype);
        
    }

    public class FirstClassTicket : Ticket, ITicket
    {
        public CabType Type { get; set; }
        public void changeTicket(CabType Newtype)
        {
            Type = Newtype;
        }
    }

    public class SecondClassTicket : Ticket, ITicket
    {
        public CabType Type { get; set; }
        public void changeTicket(CabType Newtype)
        {
            Type = Newtype;
        }
    }

    public class SleepinCabTicket : Ticket, ITicket
    {
        public CabType Type { get; set; }
        public void changeTicket(CabType Newtype)
        {
            Type = Newtype;
        }
    }

    public class CoupeTicket : Ticket, ITicket
    {
        public CabType Type { get; set; }
        public void changeTicket(CabType Newtype)
        {
            Type = Newtype;
        }
    }

    public class PlatzkartTicket : Ticket, ITicket
    {
        public CabType Type { get; set; }
        public void changeTicket(CabType Newtype)
        {
            Type = Newtype;
        }
    }
}
