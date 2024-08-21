using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using cp_11.Logic.Auth;
using cp_11.Logic.model;
using cp_11.View;
using cp_11.ViewModel.Base;
using Newtonsoft.Json;
using System.ComponentModel;

namespace cp_11.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private Visibility _seatTypeVisibility;
        private Visibility _buyButtonVisibility;
        public RelayCommand OpenLoginWindowCommand { get; }

        private void OpenLoginWindow(object obj)
        {
            if(IsLogedIn)
            {
                IsLogedIn = false;
                currentUser = null;
                return;
                
            }
            LoginWindow loginWindow = new LoginWindow();
            IsLogedIn = loginWindow.ShowDialog().Value;
            CurrentUser = loginWindow.CurrentUser; 
        }

        private bool isLogedIn;
        public bool IsLogedIn 
        {   get => isLogedIn;
            set 
            { 
                if (Set(ref isLogedIn, value)) OnPropertyChanged(nameof(ButtonName));

            }
        }

        private User currentUser;
        public User CurrentUser
        {
            get => currentUser;
            set => Set(ref currentUser, value);
        }

        public ICommand BuyTicketCommand { get; }

        public MainViewModel()
        {
            // Set initial visibility to Visible
            SeatTypeVisibility = Visibility.Visible;
            BuyButtonVisibility = Visibility.Visible;
            BuyTicketCommand = new RelayCommand<Train>(OnBuyTicket);
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            LoadSchedule();
            LoadMap();
        }

        ObservableCollection<Train> trains = new ObservableCollection<Train>();

        public ObservableCollection<Train> Trains
        {
            get => trains;
            set => Set(ref trains, value);
        }

        private ObservableCollection<Train> selectedTrains = null;
        public ObservableCollection<Train> SelectedTrains
        {
            get => selectedTrains;
            set => Set(ref selectedTrains, value);
        }
        ObservableCollection<Station> stations = new ObservableCollection<Station>();

        public ObservableCollection<Station> Stations
        {
            get => stations;
            set => Set(ref stations, value);
        }

        ObservableCollection<Station> stationsDeparture = new ObservableCollection<Station>();

        public ObservableCollection<Station> StationsDeparture
        {
            get => stationsDeparture;
            set => Set(ref stationsDeparture, value);
        }

        ObservableCollection<Station> stationsArrival = new ObservableCollection<Station>();

        public ObservableCollection<Station> StationsArrival
        {
            get => stationsArrival;
            set => Set(ref stationsArrival, value);
        }
        public Visibility SeatTypeVisibility
        {
            get => _seatTypeVisibility;
            set
            {
                _seatTypeVisibility = value;
                OnPropertyChanged(nameof(SeatTypeVisibility));
            }
        }

        public Visibility BuyButtonVisibility
        {
            get => _buyButtonVisibility;
            set
            {
                _buyButtonVisibility = value;
                OnPropertyChanged(nameof(BuyButtonVisibility));
            }
        }

        private void OnBuyTicket(Train selectedTrain)
        {
            // Show the message
            MessageBox.Show("Ticket bought!");

            // Hide the seat type selection and the buy button
            SeatTypeVisibility = Visibility.Collapsed;
            BuyButtonVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string ButtonName
        {
            get 
            { 
                if (IsLogedIn)
                    return "log out";
                return "log in";
            }

        }
        private bool LoadSchedule()
        {
            var files = Directory.GetFiles("Resources\\trains");
            if (files != null) // Якщо файли знайдено
            {
                foreach (var file in files)
                {
                    var train = new Train();
                    var lines = File.ReadAllText(file);
                    train = JsonConvert.DeserializeObject<Train>(lines);
                    Trains.Add(train);
                }

                return true;
            }

            return false;
        }

        private bool LoadMap()
        {
            if (Trains != null)
            {
                foreach (var train in Trains)
                {
                    foreach (var station in train.Stations)
                    {
                        if (!Stations.Contains(station))
                        {
                            Stations.Add(station);
                        }
                    }
                }

                Stations = new ObservableCollection<Station>(stations.DistinctBy(x => x.Name));
                StationsDeparture = new ObservableCollection<Station>(stations);
                StationsArrival = new ObservableCollection<Station>(stations);
                return true;
            }

            return false;
        }

        private Station selectedDeparture = null;

        public Station SelectedDeparture
        {
            get => selectedDeparture;
            set
            {

                if (Set(ref selectedDeparture, value))
                {
                    StationsArrival =
                        new ObservableCollection<Station>(stations.Where(x => x.Name != selectedDeparture.Name));
                    SelectTrains();
                }
            }
        }

        private Station selectedArrival = null;

        public Station SelectedArrival
        {
            get => selectedArrival;
            set
            {

                if (Set(ref selectedArrival, value))
                {
                    StationsDeparture =
                        new ObservableCollection<Station>(stations.Where(x => x.Name != selectedArrival.Name));
                    SelectTrains();
                }
            }
        }

        private void SelectTrains()
        {
            if (SelectedDeparture != null && SelectedArrival != null)
            {
                var filteredTrains = trains.Where(train =>
                {
                    var stations = train.Stations.Select(s => s.Name.Trim()).ToList();
                    int departureIndex = stations.IndexOf(selectedDeparture.Name.Trim());
                    int arrivalIndex = stations.IndexOf(selectedArrival.Name.Trim());

                    return departureIndex != -1 && arrivalIndex != -1 && departureIndex > arrivalIndex;
                }).ToList();
                foreach (var train in filteredTrains)
                {
                    train.ArrivalTime = train.Stations.First(s => s.Name == selectedArrival.Name).Departure;
                    train.DepartureTime = train.Stations.First(s => s.Name == selectedDeparture.Name).Arrival;
                }
                SelectedTrains = new ObservableCollection<Train>(filteredTrains);
            }
        }



    }
}


