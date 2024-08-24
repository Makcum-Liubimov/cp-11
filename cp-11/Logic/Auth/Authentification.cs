using cp_11.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using cp_11.Logic.model;

namespace cp_11.Logic.Auth
{
    public class Authentification
    {
        private List<User> users = new();

        public bool CheckUser(string login, string password)
        {
            var user = GetUser(login);
            if (user != null)
                return user.Hash == CreateHashString(login + "#*!" + password);
            return false;
        }

        public User GetUser(string login)
        {
            LoadUser();
            try
            {
                var item = users.First(x => x.Login == login);
                if (item.Tickets == null) item.Tickets = new List<Ticket>();
                return item;
            }
            catch (Exception e)
            {
            }

            return null;
        }

        public bool RegisterUser(string login, string password, string firstName, string lastName)
        {
            var finduser = GetUser(login);
            if (finduser == null)
            {
                var user = new User
                {
                    Login = login,
                    Hash = CreateHashString(login + "#*!" + password),
                    Tickets = new List<Ticket>(),
                    LastName = lastName,
                    FirstName = firstName
                };
                return AddUser(user);
            }

            return false;
        }

        private bool AddUser(User user)
        {
            var result = false;
            try
            {
                var item = users.First(x => x.Login == user.Login);
                //if item found the user is already exist
            }
            catch (Exception e)
            {
                users.Add(user);
                result = true;
            }

            SaveUsers();
            return result;
        }

        private string CreateHashString(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(input);
                Array.Reverse(inputBytes);
                var hashBytes = md5.ComputeHash(inputBytes);
                Array.Reverse(hashBytes);
                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++) sb.Append(hashBytes[i].ToString("X2"));

                return sb.ToString();
            }
        }

        private void LoadUser()
        {
            if (!string.IsNullOrEmpty(Settings.Default.Users))
                users = JsonConvert.DeserializeObject<List<User>>(Settings.Default.Users) ?? new List<User>();
        }
        public bool UpdateUser(User currenUser)
        {
            var user = GetUser(currenUser.Login);
            if (user != null)
            {
                user.Tickets = new List<Ticket>(currenUser.Tickets);
                SaveUsers();
                return true;
            }
            return false;
        }

        private void SaveUsers()
        {
            Settings.Default.Users = JsonConvert.SerializeObject(users);
            Settings.Default.Save();
        }
    }
}
