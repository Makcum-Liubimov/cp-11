﻿using cp_11.Logic.Auth;
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
            bool result = _authentification.CheckUser(login,password);
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
                MessageBox.Show("хибне імя ");
                return result;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Хибний пароль");
                return result;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Хиьне імя");
                return result;
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                MessageBox.Show("Хибна Фамілія");
                return result;
            }
            User SomeUser = new User();
            SomeUser.Login = login;
            SomeUser.FirstName = name;
            SomeUser.LastName = surname;
            SomeUser.Hash = password;
            if (_authentification.CheckUser(SomeUser))
            {
                MessageBox.Show("Користувач вже існує");
            }
            else
            {
                if (_authentification.RegisterUser(SomeUser))
                {
                    MessageBox.Show("Користувача створено");
                    CurrentUser = _authentification.GetUser(SomeUser);
                   
                    result = true;
                }
                else
                {
                    MessageBox.Show("Користувач вже існує");
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