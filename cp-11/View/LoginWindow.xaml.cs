using cp_11;
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
using System.Windows.Shapes;
using cp_11.ViewModel;
using System.Xml;

namespace cp_11.View
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = (LoginViewModel)DataContext;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (_viewModel.CheckUser(username, password))
            {
                MessageBox.Show($"Ви увійшли як {username}");
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var loginWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    loginWindow?.Close();

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                });
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            NameLabel.Visibility = Visibility.Visible;
            NameTextBox.Visibility = Visibility.Visible;
            SurnameLabel.Visibility = Visibility.Visible;
            SurnameTextBox.Visibility = Visibility.Visible;

            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname))
            {
               // public bool CreateUser(string login, string password)
                //{
                   // bool result = false;
                    //if (_authentification.CheckUser(login, password))
                       // MessageBox.Show("Користувача існує");
                    //else
                    //{
                       // if (_authentification.RegisterUser(login, password, "qwerty", "qwerty"))
                        //{
                           // MessageBox.Show($"Користувача {login} створено");

                            // Close the current window and redirect to the LoginWindow
                          //  Application.Current.Dispatcher.Invoke(() =>
                            //{
                              //  var signUpWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                                //signUpWindow?.Close();

                                //LoginWindow loginWindow = new LoginWindow();
                                //loginWindow.Show();
                            //});

                            //result = true;
                        //}
                        //else
                        //{
                          //  MessageBox.Show("Помилка створення користувача");
                        //}
                    //}

                   //eturn result;
                //
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть ім'я та прізвище для реєстрації.");
            }
        }
    }
}
