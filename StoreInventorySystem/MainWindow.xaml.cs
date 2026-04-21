using System.Windows;
using StoreInventorySystem.Views;

namespace StoreInventorySystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Завантажуємо збережені налаштування (тема, мова)
            var settings = Services.SettingsService.Load();
            App.ApplyTheme(settings.Theme);
            App.ApplyLanguage(settings.Language);

            // Стартуємо зі сторінки входу
            MainFrame.Navigate(new WelcomePage());
        }

        // Навігація на панель керування після успішного логіну
        public void NavigateToDashboard()
        {
            MainFrame.Navigate(new MainDashboardPage());
        }
    }
}