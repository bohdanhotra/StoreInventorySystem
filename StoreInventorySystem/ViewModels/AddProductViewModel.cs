using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// ViewModel сторінки додавання нового товару.
    /// Виконує валідацію полів, збереження картинки та запис у JSON.
    /// </summary>
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private Product _newProduct = new Product();
        private string _errorMessage;

        /// <summary>Новий товар, що заповнюється у формі.</summary>
        public Product NewProduct
        {
            get => _newProduct;
            set { _newProduct = value; OnPropertyChanged(); }
        }

        /// <summary>Повідомлення про помилку валідації форми.</summary>
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

        /// <summary>Команда відкриття діалогу вибору зображення.</summary>
        public ICommand SelectImageCommand { get; }

        /// <summary>Команда збереження нового товару після валідації.</summary>
        public ICommand SaveCommand { get; }

        public AddProductViewModel()
        {
            SelectImageCommand = new RelayCommand(_ =>
            {
                var dlg = new OpenFileDialog
                {
                    Filter = "Зображення (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif"
                };
                if (dlg.ShowDialog() == true)
                    NewProduct.ImagePath = dlg.FileName;
            });

            SaveCommand = new RelayCommand(_ =>
            {
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

                if (!string.IsNullOrEmpty(NewProduct.ImagePath))
                    NewProduct.ImagePath = ProductService.SaveImage(NewProduct.ImagePath);

                NewProduct.Id = Guid.NewGuid().ToString("N").Substring(0, 8);
                products.Add(NewProduct);
                ProductService.SaveProducts(products);

                MessageBox.Show("Товар успішно збережено!", "Збереження",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                NewProduct = new Product();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}