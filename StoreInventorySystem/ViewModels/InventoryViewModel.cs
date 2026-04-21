using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using StoreInventorySystem.Models;
using StoreInventorySystem.Services;
using StoreInventorySystem.Commands;

namespace StoreInventorySystem.ViewModels
{
    public class InventoryViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public ICommand DeleteCommand { get; }

        public InventoryViewModel()
        {
            // Завантажуємо список товарів із нашого сервісу (з JSON файлу)
            var loadedProducts = ProductService.LoadProducts();
            Products = new ObservableCollection<Product>(loadedProducts);

            // Команда видалення товару
            DeleteCommand = new RelayCommand(obj =>
            {
                if (obj is Product product)
                {
                    Products.Remove(product);
                    // Оновлюємо файл JSON після видалення
                    ProductService.SaveProducts(Products.ToList());
                }
            });
        }
    }
}