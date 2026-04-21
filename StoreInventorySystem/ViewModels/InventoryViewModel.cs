using System.Collections.ObjectModel;
using System.Collections.Generic;
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
    public class InventoryViewModel : INotifyPropertyChanged
    {
        // Повна колекція товарів
        private List<Product> _allProducts;

        // Відфільтрована колекція для відображення
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        // Рядок пошуку
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        // Вибрана категорія для фільтру
        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        // Список категорій для ComboBox (+ "Всі категорії")
        public ObservableCollection<string> Categories { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ExportCsvCommand { get; }

        public InventoryViewModel()
        {
            _allProducts = ProductService.LoadProducts();
            Products = new ObservableCollection<Product>(_allProducts);

            // Збираємо унікальні категорії
            var cats = _allProducts.Select(p => p.Category)
                                   .Where(c => !string.IsNullOrEmpty(c))
                                   .Distinct()
                                   .OrderBy(c => c)
                                   .ToList();
            cats.Insert(0, ""); // "" = всі категорії
            Categories = new ObservableCollection<string>(cats);
            SelectedCategory = "";

            // Команда видалення з підтвердженням
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

            // Команда редагування — навігація на EditProductPage
            EditCommand = new RelayCommand(obj =>
            {
                if (obj is not Product product) return;
                var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
                // Знаходимо Frame dashboardу через MainDashboardPage
                var dashboard = mainWindow?.MainFrame.Content as Views.MainDashboardPage;
                dashboard?.ContentFrame.Navigate(new Views.EditProductPage(product));
            });

            // Команда експорту CSV
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

        // Фільтрація за пошуком та категорією
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