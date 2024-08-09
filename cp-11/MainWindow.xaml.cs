using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cp_11
{
    public partial class MainWindow : Window
    {
        private Scedule_Nod[] schedule;
        private List<Scedule_Nod> userTickets;

        public MainWindow()
        {
            InitializeComponent();
            userTickets = new List<Scedule_Nod>();
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            schedule = new Scedule_Nod[]
            {
                new Scedule_Nod { IsTrainAvailableToday = true, NumberOfTrain = 1, Destination = "Kyiv" },
                new Scedule_Nod { IsTrainAvailableToday = false, NumberOfTrain = 2, Destination = "Lviv" },
                new Scedule_Nod { IsTrainAvailableToday = true, NumberOfTrain = 3, Destination = "Odessa" }
            };
        }

        private void ShowScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleListBox.Items.Clear();
            for (int i = 0; i < schedule.Length; i++)
            {
                var node = schedule[i];
                ScheduleListBox.Items.Add($"Елемент {i + 1}: Потяг №{node.NumberOfTrain}, Пункт призначення: {node.Destination}, Доступний сьогодні: {node.IsTrainAvailableToday}");
            }
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleListBox.SelectedItem != null)
            {
                int selectedIndex = ScheduleListBox.SelectedIndex;
                if (schedule[selectedIndex].IsTrainAvailableToday)
                {
                    userTickets.Add(schedule[selectedIndex]);
                    MessageBox.Show("Квиток придбано!");
                    MyTicketsButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Потяг сьогодні недоступний.");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть потяг з розкладу.");
            }
        }

        private void MyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            TicketsListBox.Items.Clear();
            foreach (var ticket in userTickets)
            {
                TicketsListBox.Items.Add($"Потяг №{ticket.NumberOfTrain}, Пункт призначення: {ticket.Destination}");
            }
        }
    }
}
