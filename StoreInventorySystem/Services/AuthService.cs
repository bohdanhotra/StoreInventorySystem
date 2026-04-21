using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.Services
{
    public static class AuthService
    {
        private const string FilePath = "users.json";
        private static List<User> _users = LoadUsers();

        public static User CurrentUser { get; private set; }

        // Метод для завантаження користувачів із JSON
        private static List<User> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch { return new List<User>(); }
        }

        // Метод для збереження користувачів у JSON
        private static void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users);
            File.WriteAllText(FilePath, json);
        }

        // Хешування пароля (SHA256)
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool Login(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }
        public static void Logout()
        {
            CurrentUser = null;
        }
        public static bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username)) return false;

            _users.Add(new User
            {
                Id = _users.Count + 1,
                Username = username,
                Password = HashPassword(password), // Зберігаємо хеш
                Role = "Manager"
            });

            SaveUsers(); // Записуємо у файл
            return true;
        }
    }
}