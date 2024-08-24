﻿using System;
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
        public RelayCommand OpenMyTicketsWindowCommand { get; }
        private void OpenMyTicketsWindow(object obj)
        {
            MyTicketsViewModel myTicketsViewModel = new MyTicketsViewModel();
            myTicketsViewModel.Tickets = currentUser.Tickets;
            MyTicketsWindow myTicketsWindow = new MyTicketsWindow();
            myTicketsWindow.DataContext = myTicketsViewModel;
            myTicketsWindow.ShowDialog();
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
        private void BuyTicket(Train selectedTrain)
        {   
               MyTicket = new Ticket();
            MyTicket.TrainNumber = selectedTrain.NumberOfTrain;
            MyTicket.Source = SelectedArrival.Name;
            MyTicket.Destintaion = SelectedDeparture.Name;
            MyTicket.TimeOfDeparture = SelectedDeparture.Arrival;
            MyTicket.TimeOfArrival = SelectedArrival.Departure;
            MyTicket.PassengerName = CurrentUser.FirstName +" " + CurrentUser.LastName;

            int destinationDist = 0;
            int arrivalDist = 0;
            foreach (var station in selectedTrain.Stations)
            {
                if (station.Name == SelectedArrival.Name)
                {
                    if (station.Distance.Contains("-")) destinationDist = 0;
                    else int.TryParse(station.Distance, out destinationDist);
                    MyTicket.TimeOfDeparture = station.Departure;
                }

                if (station.Name == selectedDeparture.Name)
                {
                    if (station.Distance.Contains("-")) arrivalDist = 0;
                    else int.TryParse(station.Distance, out arrivalDist);
                    MyTicket.TimeOfArrival = station.Arrival;
                }

               
            }
            MyTicket.Cost = 100 + (arrivalDist - destinationDist);

            
            
            Random random = new Random();
            MyTicket.Seat = random.Next(1, 100);
            MyTicket.Cab = random.Next(1, 10);
            currentUser.Tickets.Add(MyTicket);
            authentification.UpdateUser(currentUser);

            //MessageBox.Show("Ticket bought!");

            //MessageBox.Show("Login to buy ticket");

        }

        private Ticket MyTicket = null;
        private Authentification authentification = new Authentification();
        public MainViewModel()
        {
            OpenMyTicketsWindowCommand = new RelayCommand(OpenMyTicketsWindow);
            BuyTicketCommand = new RelayCommand<Train>(null, BuyTicket);
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
                    int index = 0;
                    train.Seats = new int[40];
                    for (int i = 1; i < 40; i++)
                    {
                        if(new Random().Next(0,2) == 1) train.Seats[index++] = i;


                    }

                    int count = train.Seats.Select(x => x != 0).Count();
                    //Array.Resize(ref train.Seats,count);
                    int cabCount = new Random().Next(15, 30);
                    train.Cabs = new int[cabCount];
                    for (int i = 1; i < cabCount+1; i++)
                    {
                        train.Cabs[i-1] = i;
                    }

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


