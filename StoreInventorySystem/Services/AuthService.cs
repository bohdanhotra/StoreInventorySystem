using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.Services
{
    /// <summary>
    /// Статичний сервіс авторизації та реєстрації користувачів.
    /// Зберігає облікові дані у файлі users.json поряд з виконуваним файлом.
    /// Паролі зберігаються у вигляді SHA-256 хешу.
    /// </summary>
    public static class AuthService
    {
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");

        private static List<User> _users = LoadUsers();

        /// <summary>Поточний авторизований користувач (null якщо не увійшов).</summary>
        public static User CurrentUser { get; private set; }

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

        private static void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Обчислює SHA-256 хеш пароля у форматі Base64.
        /// </summary>
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Перевіряє облікові дані та встановлює CurrentUser при успіху.
        /// </summary>
        /// <returns>true — якщо авторизація успішна.</returns>
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

        /// <summary>Скидає поточного користувача (вихід з системи).</summary>
        public static void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Реєструє нового користувача з роллю Manager.
        /// </summary>
        /// <returns>true — якщо реєстрація успішна (логін ще не зайнятий).</returns>
        public static bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username)) return false;

            _users.Add(new User
            {
                Id       = _users.Count + 1,
                Username = username,
                Password = HashPassword(password),
                Role     = "Manager"
            });

            SaveUsers();
            return true;
        }
    }
}