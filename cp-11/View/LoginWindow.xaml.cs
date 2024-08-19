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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            //проверку на то что пароль и юзернейм не пустие


            //залишаємо Click бо Password не можно  Binding
            if (DataContext is LoginViewModel loginViewModel)
            {
                if (loginViewModel.CheckUser(UsernameTextBox.Text, PasswordBox.Password))
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    //написать месседжбокс "неправильний пароль или юзернейм"
                }
            }




            /*string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string userType = (UserTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (UserStore.IsValidUser(username, password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильне ім'я користувача або пароль.");
            }*/
        }

        private void CreateUser_OnClick(object sender, RoutedEventArgs e)
        {
            //залишаємо Click бо Password не можно  Binding
            if (DataContext is LoginViewModel loginViewModel)
            {
                if (loginViewModel.CreateUser(UsernameTextBox.Text, PasswordBox.Password))
                {
                    DialogResult = true;
                    Close();
                }
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            //проверка всех 4ех полей на пустоту


            // If Name and Surname fields are hidden, show them
            if (NameTextBox.Visibility == Visibility.Collapsed && SurnameTextBox.Visibility == Visibility.Collapsed)
            {
                NameLabel.Visibility = Visibility.Visible;
                NameTextBox.Visibility = Visibility.Visible;
                SurnameLabel.Visibility = Visibility.Visible;
                SurnameTextBox.Visibility = Visibility.Visible;
                SignInButton.Content = "Sign Up";
            }
            else
            {


                // Sign up logic here
                if (DataContext is LoginViewModel loginViewModel)
                {
                    if (loginViewModel.CreateUser(UsernameTextBox.Text, PasswordBox.Password))
                    {
                        DialogResult = true;
                        Close();
                    }
                    else
                    {

                        //меседжбок "користувач вже існує"

                    }
                }
            }
        }
    }
}
