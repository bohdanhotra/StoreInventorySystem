using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Models;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    public class EditProductViewModel : INotifyPropertyChanged
    {
        // Оригінальний Id товару для знаходження у списку
        private readonly string _originalId;

        private Product _product;
        public Product Product
        {
            get => _product;
            set { _product = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        // Список категорій для ComboBox
        public ObservableCollection<string> CategoryList { get; } = new ObservableCollection<string>
        {
            "Електроніка", "Одяг", "Продукти", "Побутова хімія",
            "Інструменти", "Спорт", "Книги", "Інше"
        };

        public ICommand SelectImageCommand { get; }
        public ICommand SaveCommand { get; }

        public EditProductViewModel(Product productToEdit)
        {
            _originalId = productToEdit.Id;

            // Робимо копію об'єкта щоб не змінювати оригінал до збереження
            Product = new Product
            {
                Id          = productToEdit.Id,
                Name        = productToEdit.Name,
                Category    = productToEdit.Category,
                Price       = productToEdit.Price,
                Quantity    = productToEdit.Quantity,
                Description = productToEdit.Description,
                ImagePath   = productToEdit.ImagePath
            };

            // Вибір нового зображення
            SelectImageCommand = new RelayCommand(_ =>
            {
                var dlg = new OpenFileDialog
                {
                    Filter = "Зображення (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif"
                };
                if (dlg.ShowDialog() == true)
                    Product.ImagePath = dlg.FileName;
            });

            // Збереження змін
            SaveCommand = new RelayCommand(_ =>
            {
                // Валідація
                if (string.IsNullOrWhiteSpace(Product.Name))
                {
                    ErrorMessage = "Введіть назву товару!";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Product.Category))
                {
                    ErrorMessage = "Оберіть категорію!";
                    return;
                }
                if (Product.Price <= 0)
                {
                    ErrorMessage = "Ціна має бути більше 0!";
                    return;
                }
                if (Product.Quantity < 0)
                {
                    ErrorMessage = "Кількість не може бути від'ємною!";
                    return;
                }

                ErrorMessage = "";

                // Копіюємо нову картинку якщо шлях не є абсолютним (вже скопійований)
                if (!string.IsNullOrEmpty(Product.ImagePath) &&
                    !Product.ImagePath.StartsWith(AppDomain.CurrentDomain.BaseDirectory))
                {
                    Product.ImagePath = ProductService.SaveImage(Product.ImagePath);
                }

                // Знаходимо та оновлюємо товар у списку
                var products = ProductService.LoadProducts();
                var existing = products.FirstOrDefault(p => p.Id == _originalId);
                if (existing != null)
                {
                    existing.Name        = Product.Name;
                    existing.Category    = Product.Category;
                    existing.Price       = Product.Price;
                    existing.Quantity    = Product.Quantity;
                    existing.Description = Product.Description;
                    existing.ImagePath   = Product.ImagePath;
                }

                ProductService.SaveProducts(products);

                MessageBox.Show("Товар успішно оновлено!", "Збереження",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Повертаємось на список товарів
                var mainWindow = Application.Current.MainWindow as MainWindow;
                var dashboard = mainWindow?.MainFrame.Content as Views.MainDashboardPage;
                dashboard?.ContentFrame.Navigate(new Views.InventoryPage());
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
