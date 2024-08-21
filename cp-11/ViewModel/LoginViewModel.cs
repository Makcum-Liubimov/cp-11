using cp_11.Logic.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cp_11.ViewModel.Base;
using System.Windows;
using cp_11.View;

namespace cp_11.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private readonly Authentification _authentification;

        public LoginViewModel()
        {
            _authentification = new Authentification();
        }

        public bool CheckUser(string login, string password)
        {
            bool result = _authentification.CheckUser(login, password);
            if (result)
            {
                MessageBox.Show($"Ви увійшли як {login}");
                CurrentUser = _authentification.GetUser(login);
                
            }
               
            else
                MessageBox.Show("Неправильне ім'я користувача або пароль.");
            return result;
        }

        public bool CreateUser(string login, string password, string name, string surname)
        {
            bool result = false;
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Wrong login");
                return result;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Wrong password");
                return result;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Wrong name");
                return result;
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                MessageBox.Show("Wrong surname");
                return result;
            }
            if (_authentification.CheckUser(login, password))
            {
                MessageBox.Show("Користувач вже існує");
            }
            else
            {
                if (_authentification.RegisterUser(login, password, name, surname))
                {
                    MessageBox.Show("Користувача створено");
                    CurrentUser = _authentification.GetUser(login);
                   
                    result = true;
                }
                else
                {
                    MessageBox.Show("Помилка створення користувача");
                }
            }

            return result;
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
    }
}