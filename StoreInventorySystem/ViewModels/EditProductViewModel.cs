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
    /// <summary>
    /// ViewModel сторінки редагування існуючого товару.
    /// Отримує копію товару, дозволяє змінити поля та зберігає результат.
    /// </summary>
    public class EditProductViewModel : INotifyPropertyChanged
    {
        private readonly string _originalId;
        private Product _product;
        private string _errorMessage;

        /// <summary>Редагована копія товару (не змінює оригінал до збереження).</summary>
        public Product Product
        {
            get => _product;
            set { _product = value; OnPropertyChanged(); }
        }

        /// <summary>Повідомлення про помилку валідації.</summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        /// <summary>Список доступних категорій для ComboBox.</summary>
        public ObservableCollection<string> CategoryList { get; } = new ObservableCollection<string>
        {
            "Електроніка", "Одяг", "Продукти", "Побутова хімія",
            "Інструменти", "Спорт", "Книги", "Інше"
        };

        /// <summary>Команда відкриття діалогу вибору нового зображення.</summary>
        public ICommand SelectImageCommand { get; }

        /// <summary>Команда збереження змін та повернення на список товарів.</summary>
        public ICommand SaveCommand { get; }

        /// <param name="productToEdit">Товар, що потрібно відредагувати.</param>
        public EditProductViewModel(Product productToEdit)
        {
            _originalId = productToEdit.Id;

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

            SelectImageCommand = new RelayCommand(_ =>
            {
                var dlg = new OpenFileDialog
                {
                    Filter = "Зображення (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif"
                };
                if (dlg.ShowDialog() == true)
                    Product.ImagePath = dlg.FileName;
            });

            SaveCommand = new RelayCommand(_ =>
            {
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

                if (!string.IsNullOrEmpty(Product.ImagePath) &&
                    !Product.ImagePath.StartsWith(AppDomain.CurrentDomain.BaseDirectory))
                {
                    Product.ImagePath = ProductService.SaveImage(Product.ImagePath);
                }

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
