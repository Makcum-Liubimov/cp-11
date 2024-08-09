using System.Collections.Generic;

namespace cp_11
{
    public static partial class UserStore
    {
        // Проста демонстраційна база даних користувачів у вигляді словника
        private static Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "passenger", "password123" },  // Приклад користувача
            { "driver", "password456" }      // Інший приклад користувача
        };

        public static bool IsValidUser(string username, string password)
        {
            return users.ContainsKey(username) && users[username] == password;
        }

        // Метод для додавання нового користувача
        public static bool RegisterUser(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                users.Add(username, password);
                return true;
            }
            return false; // Користувач вже існує
        }
    }
}
