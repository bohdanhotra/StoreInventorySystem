using System.IO;
using System.Text.Json;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.Services
{
    /// <summary>
    /// Сервіс збереження та завантаження налаштувань програми.
    /// Зберігає файл settings.json поряд з виконуваним файлом.
    /// </summary>
    public static class SettingsService
    {
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

        /// <summary>Завантажує налаштування з файлу або повертає значення за замовчуванням.</summary>
        public static AppSettings Load()
        {
            if (!File.Exists(FilePath)) return new AppSettings();
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            catch
            {
                return new AppSettings();
            }
        }

        /// <summary>Зберігає налаштування у файл settings.json.</summary>
        public static void Save(AppSettings settings)
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
