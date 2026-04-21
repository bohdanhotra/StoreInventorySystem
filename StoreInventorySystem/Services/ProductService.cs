using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.Services
{
    /// <summary>
    /// Статичний сервіс для роботи з каталогом товарів.
    /// Читає та записує products.json, копіює зображення у папку ProductImages,
    /// надає метод експорту у CSV.
    /// </summary>
    public static class ProductService
    {
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");

        private static readonly string ImageFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductImages");

        static ProductService()
        {
            if (!Directory.Exists(ImageFolder))
                Directory.CreateDirectory(ImageFolder);
        }

        /// <summary>Завантажує список товарів із JSON-файлу.</summary>
        public static List<Product> LoadProducts()
        {
            if (!File.Exists(FilePath)) return new List<Product>();
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
            catch
            {
                return new List<Product>();
            }
        }

        /// <summary>Зберігає список товарів у JSON-файл.</summary>
        public static void SaveProducts(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Копіює зображення товару в папку ProductImages і повертає новий шлях.
        /// Повертає null якщо файл не знайдено або шлях порожній.
        /// </summary>
        public static string SaveImage(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
                return null;

            string ext = Path.GetExtension(sourcePath);
            string fileName = Guid.NewGuid().ToString("N") + ext;
            string destPath = Path.Combine(ImageFolder, fileName);
            File.Copy(sourcePath, destPath, overwrite: true);
            return destPath;
        }

        /// <summary>
        /// Експортує список товарів у CSV-файл із заголовком.
        /// Поля з комами або лапками екрануються відповідно до стандарту RFC 4180.
        /// </summary>
        public static void ExportToCsv(List<Product> products, string csvPath)
        {
            var lines = new List<string>();
            lines.Add("Id,Name,Category,Price,Quantity,Description");

            foreach (var p in products)
            {
                string line = $"{p.Id},{Escape(p.Name)},{Escape(p.Category)},{p.Price},{p.Quantity},{Escape(p.Description)}";
                lines.Add(line);
            }

            File.WriteAllLines(csvPath, lines, System.Text.Encoding.UTF8);
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Contains(',') || value.Contains('"'))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
    }
}