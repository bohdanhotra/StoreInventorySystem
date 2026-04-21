using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    /// <summary>
    /// ViewModel головної сторінки (Dashboard).
    /// Обчислює та показує зведену статистику по товарах на складі.
    /// </summary>
    public class HomeViewModel : INotifyPropertyChanged
    {
        private int _totalProducts;
        private int _totalItems;
        private decimal _totalValue;
        private int _lowStockCount;
        private string _welcomeText;

        /// <summary>Кількість унікальних позицій товарів.</summary>
        public int TotalProducts
        {
            get => _totalProducts;
            set { _totalProducts = value; OnPropertyChanged(); }
        }

        /// <summary>Загальна кількість одиниць товару на складі.</summary>
        public int TotalItems
        {
            get => _totalItems;
            set { _totalItems = value; OnPropertyChanged(); }
        }

        /// <summary>Загальна вартість усього складу.</summary>
        public decimal TotalValue
        {
            get => _totalValue;
            set { _totalValue = value; OnPropertyChanged(); }
        }

        /// <summary>Кількість позицій з малим залишком (менше 5 одиниць).</summary>
        public int LowStockCount
        {
            get => _lowStockCount;
            set { _lowStockCount = value; OnPropertyChanged(); }
        }

        /// <summary>Привітальний текст з іменем поточного користувача.</summary>
        public string WelcomeText
        {
            get => _welcomeText;
            set { _welcomeText = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            string username = AuthService.CurrentUser?.Username ?? "Користувач";
            WelcomeText = $"Вітаємо, {username}!";

            var products = ProductService.LoadProducts();
            TotalProducts = products.Count;
            TotalItems    = products.Sum(p => p.Quantity);
            TotalValue    = products.Sum(p => p.Price * p.Quantity);
            LowStockCount = products.Count(p => p.Quantity < 5);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
