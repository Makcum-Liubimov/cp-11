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
using cp_11.Logic.Auth;

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

            DialogResult = _viewModel.CheckUser(username, password);
            Close();
        }

        private bool IsSingIn = true;
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            if(IsSingIn)
            {
                RegestrationWindow.Height = 350;
                NameLabel.Visibility = Visibility.Visible;
                NameTextBox.Visibility = Visibility.Visible;
                SurnameLabel.Visibility = Visibility.Visible;
                SurnameTextBox.Visibility = Visibility.Visible;
                SignInButton.Content = "Sing Up";
                IsSingIn = false;
                return;
            }
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;

            
            DialogResult = _viewModel.CreateUser(username,password,name,surname);
            
            Close();
        }

        public User CurrentUser { get => _viewModel.CurrentUser; }
    }
}
