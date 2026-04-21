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
        // Зберігаємо users.json поряд із exe
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");

        private static List<User> _users = LoadUsers();

        public static User CurrentUser { get; private set; }

        // Завантаження з JSON
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

        // Збереження у JSON
        private static void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // Хешування пароля SHA256
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public static bool Login(string username, string password)
        {
            string hash = HashPassword(password);
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == hash);
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
                Password = HashPassword(password), // Зберігаємо хеш, не пароль
                Role = "Manager"
            });

            SaveUsers();
            return true;
        }
    }
}