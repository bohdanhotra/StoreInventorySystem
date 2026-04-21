using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private int _totalProducts;
        private int _totalItems;
        private decimal _totalValue;
        private int _lowStockCount;
        private string _welcomeText;

        public int TotalProducts
        {
            get => _totalProducts;
            set { _totalProducts = value; OnPropertyChanged(); }
        }

        public int TotalItems
        {
            get => _totalItems;
            set { _totalItems = value; OnPropertyChanged(); }
        }

        public decimal TotalValue
        {
            get => _totalValue;
            set { _totalValue = value; OnPropertyChanged(); }
        }

        public int LowStockCount
        {
            get => _lowStockCount;
            set { _lowStockCount = value; OnPropertyChanged(); }
        }

        public string WelcomeText
        {
            get => _welcomeText;
            set { _welcomeText = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            // Формуємо привітання з іменем поточного користувача
            string username = AuthService.CurrentUser?.Username ?? "Користувач";
            WelcomeText = $"Вітаємо, {username}!";

            // Рахуємо статистику
            var products = ProductService.LoadProducts();
            TotalProducts  = products.Count;
            TotalItems     = products.Sum(p => p.Quantity);
            TotalValue     = products.Sum(p => p.Price * p.Quantity);
            LowStockCount  = products.Count(p => p.Quantity < 5);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
