using System.Collections.Generic;
using System.Linq;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.Services
{
    public static class AuthService
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "123", Role = "Admin" }
        };

        public static User CurrentUser { get; private set; }

        public static bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public static bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username)) return false; // Користувач вже існує

            _users.Add(new User
            {
                Id = _users.Count + 1,
                Username = username,
                Password = password,
                Role = "Manager"
            });
            return true;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}