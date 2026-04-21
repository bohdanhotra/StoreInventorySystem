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
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private Product _newProduct = new Product();
        public Product NewProduct
        {
            get => _newProduct;
            set { _newProduct = value; OnPropertyChanged(); }
        }

        // Повідомлення про помилку для відображення у View
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

        public AddProductViewModel()
        {
            // Вибір картинки через діалог
            SelectImageCommand = new RelayCommand(_ =>
            {
                var dlg = new OpenFileDialog
                {
                    Filter = "Зображення (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif"
                };
                if (dlg.ShowDialog() == true)
                    NewProduct.ImagePath = dlg.FileName;
            });

            // Збереження товару
            SaveCommand = new RelayCommand(_ =>
            {
                // Валідація
                if (string.IsNullOrWhiteSpace(NewProduct.Name))
                {
                    ErrorMessage = "Введіть назву товару!";
                    return;
                }
                if (string.IsNullOrWhiteSpace(NewProduct.Category))
                {
                    ErrorMessage = "Оберіть категорію!";
                    return;
                }
                if (NewProduct.Price <= 0)
                {
                    ErrorMessage = "Ціна має бути більше 0!";
                    return;
                }
                if (NewProduct.Quantity < 0)
                {
                    ErrorMessage = "Кількість не може бути від'ємною!";
                    return;
                }

                ErrorMessage = "";

                var products = ProductService.LoadProducts();

                // Копіюємо картинку у папку програми
                if (!string.IsNullOrEmpty(NewProduct.ImagePath))
                    NewProduct.ImagePath = ProductService.SaveImage(NewProduct.ImagePath);

                NewProduct.Id = Guid.NewGuid().ToString("N").Substring(0, 8);
                products.Add(NewProduct);
                ProductService.SaveProducts(products);

                MessageBox.Show("Товар успішно збережено!", "Збереження",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Скидаємо форму після збереження
                NewProduct = new Product();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}