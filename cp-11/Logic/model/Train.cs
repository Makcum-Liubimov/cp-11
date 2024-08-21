using cp_11.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_11.Logic.model
{
    public class Train : BindableBase
    {
        public List<Station> Stations { get; set; }
        public string StartStation { get; set; }
        public string FinishStation { get; set; }
        public string NumberOfTrain { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }

        // Seat Types
        public List<string> SeatTypes { get; set; } = new List<string> { "Platzkart", "Kupe", "Esve" };

        private string _selectedSeatType;
        public string SelectedSeatType
        {
            get => _selectedSeatType;
            set
            {
                Set(ref _selectedSeatType, value);
                OnPropertyChanged(nameof(IsSeatTypeSelected));
            }
        }

        public bool IsSeatTypeSelected => !string.IsNullOrEmpty(SelectedSeatType);

    }
}
