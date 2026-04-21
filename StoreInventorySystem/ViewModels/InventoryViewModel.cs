using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Win32;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Models;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    /// <summary>
    /// ViewModel сторінки списку товарів.
    /// Підтримує пошук по назві, фільтрацію по категорії,
    /// видалення з підтвердженням, редагування та експорт у CSV.
    /// </summary>
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private List<Product> _allProducts;
        private ObservableCollection<Product> _products;
        private string _searchText = "";
        private string _selectedCategory;

        /// <summary>Відфільтрована колекція товарів для відображення в таблиці.</summary>
        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        /// <summary>Рядок пошуку за назвою товару.</summary>
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); ApplyFilter(); }
        }

        /// <summary>Вибрана категорія для фільтрації (порожній рядок = усі).</summary>
        public string SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); ApplyFilter(); }
        }

        /// <summary>Список доступних категорій для ComboBox фільтру.</summary>
        public ObservableCollection<string> Categories { get; set; }

        /// <summary>Команда видалення товару (з діалогом підтвердження).</summary>
        public ICommand DeleteCommand { get; }

        /// <summary>Команда переходу на сторінку редагування товару.</summary>
        public ICommand EditCommand { get; }

        /// <summary>Команда експорту поточного списку товарів у CSV-файл.</summary>
        public ICommand ExportCsvCommand { get; }

        public InventoryViewModel()
        {
            _allProducts = ProductService.LoadProducts();
            Products = new ObservableCollection<Product>(_allProducts);

            var cats = _allProducts
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();
            cats.Insert(0, "");
            Categories = new ObservableCollection<string>(cats);
            SelectedCategory = "";

            DeleteCommand = new RelayCommand(obj =>
            {
                if (obj is not Product product) return;

                var result = System.Windows.MessageBox.Show(
                    "Ви впевнені, що хочете видалити цей товар?",
                    "Видалити товар?",
                    System.Windows.MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Warning);

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    _allProducts.Remove(product);
                    Products.Remove(product);
                    ProductService.SaveProducts(_allProducts);
                }
            });

            EditCommand = new RelayCommand(obj =>
            {
                if (obj is not Product product) return;
                var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
                var dashboard = mainWindow?.MainFrame.Content as Views.MainDashboardPage;
                dashboard?.ContentFrame.Navigate(new Views.EditProductPage(product));
            });

            ExportCsvCommand = new RelayCommand(_ =>
            {
                var dlg = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = "products.csv"
                };
                if (dlg.ShowDialog() == true)
                {
                    ProductService.ExportToCsv(_allProducts, dlg.FileName);
                    System.Windows.MessageBox.Show("Файл збережено!", "Експорт",
                        System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            });
        }

        /// <summary>
        /// Фільтрує список товарів за текстом пошуку та вибраною категорією.
        /// </summary>
        private void ApplyFilter()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => p.Name != null &&
                                               p.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(SelectedCategory))
                filtered = filtered.Where(p => p.Category == SelectedCategory);

            Products = new ObservableCollection<Product>(filtered);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}