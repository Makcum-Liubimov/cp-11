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
using System.IO;


namespace cp_11
{
    public partial class MainWindow : Window
    {
        public List<ScheduleNode> ScheduleNodes { get; set; } = new List<ScheduleNode>();

        public MainWindow()
        {
            InitializeComponent();
            LoadCities();
            SetDefaultDate();
            LoadScheduleData("E:\\maxes_stuff\\GX_DW\\cp-scdl - list1.csv");
        }
        private void SetDefaultDate()
        {
            //DepartureDatePicker.SelectedDate = DateTime.Today;
        }
        private void LoadCities()
        {
            FromCityComboBox.Items.Add("Київ");
            //FromCityComboBox.Items.Add("Львів");
            //FromCityComboBox.Items.Add("Одеса");
            ToCityComboBox.Items.Add("Харків");
            ToCityComboBox.Items.Add("Чернігів");
          
        }
        private void LoadScheduleData(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines.Skip(1)) // Пропускаємо заголовок
                {
                    var values = line.Split(',');


                    // Додаємо у поля "Звідки" і "Куди"
                    if (!FromCityComboBox.Items.Contains(values[0]))
                    {
                        FromCityComboBox.Items.Add(values[0]);
                    }

                    if (!ToCityComboBox.Items.Contains(values[1]))
                    {
                        ToCityComboBox.Items.Add(values[1]);
                    }
                    // Додаємо дати у відповідний список
                    if (!DepartureDateComboBox.Items.Contains(values[3]))
                    {
                        DepartureDateComboBox.Items.Add(values[3]);
                    }
                    // Додаємо час у відповідний список
                    DepartureTimeComboBox.Items.Add(values[2]);

                    //ScheduleNodes.Add(scheduleNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні даних: {ex.Message}");
            }
        }

        private void ShowTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            if (FromCityComboBox.SelectedItem != null)
            {
                string selectedCity = FromCityComboBox.SelectedItem.ToString();
                DisplayTickets(selectedCity);
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть місто.");
            }
        }

        private void DisplayTickets(string city)
        {
            var tickets = ScheduleNodes.Where(s => s.FromCity == city).ToList();
            TicketsListBox.ItemsSource = tickets;
        }
    }

    public class ScheduleNode
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string TrainNumber { get; set; }
    }
}
