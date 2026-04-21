using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.Services
{
    public static class ProductService
    {
        private const string FilePath = "products.json";
        private const string ImageFolder = "ProductImages";

        static ProductService()
        {
            if (!Directory.Exists(ImageFolder)) Directory.CreateDirectory(ImageFolder);
        }

        public static List<Product> LoadProducts()
        {
            if (!File.Exists(FilePath)) return new List<Product>();
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public static void SaveProducts(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // Метод для копіювання картинки в папку програми
        public static string SaveImage(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath)) return null;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourcePath);
            string destPath = Path.Combine(ImageFolder, fileName);
            File.Copy(sourcePath, destPath);

            // Повертаємо відносний шлях для збереження в JSON
            return Path.GetFullPath(destPath);
        }
    }
}