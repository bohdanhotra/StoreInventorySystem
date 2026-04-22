using System.Windows;
using StoreInventorySystem.Views;

namespace StoreInventorySystem
{
    /// <summary>
    /// Головне вікно застосунку. Містить Frame для навігації між сторінками
    /// та завантажує збережені налаштування при запуску.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           
        }

        /// <summary>
        /// Навігація на панель керування після успішного входу.
        /// </summary>
        public void NavigateToDashboard()
        {
            MainFrame.Navigate(new MainDashboardPage());
        }
    }
}