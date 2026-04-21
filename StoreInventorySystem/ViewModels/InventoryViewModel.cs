using System.Collections.ObjectModel;
using StoreInventorySystem.Models;

namespace StoreInventorySystem.ViewModels
{
    public class InventoryViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        public InventoryViewModel()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Ноутбук Dell XPS 15", Price = 45000, Quantity = 5 },
                new Product { Id = 2, Name = "Мишка Logitech G Pro", Price = 3500, Quantity = 12 },
                new Product { Id = 3, Name = "Клавіатура Keychron K2", Price = 4200, Quantity = 8 }
            };
        }
    }
}
