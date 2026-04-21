using System.Windows;
using System.Windows.Controls;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.Views
{
    public partial class MainDashboardPage : Page
    {
        public MainDashboardPage()
        {
            InitializeComponent();

            // Показуємо ім'я та роль поточного користувача
            UserNameText.Text = AuthService.CurrentUser?.Username ?? "Користувач";
            UserRoleText.Text = AuthService.CurrentUser?.Role ?? "";

            // За замовчуванням відкриваємо головну сторінку
            ContentFrame.Navigate(new HomePage());
        }

        private void NavHome_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new HomePage());
        }

        private void NavInventory_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new InventoryPage());
        }

        private void NavAdd_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AddProductPage());
        }

        private void NavSettings_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new SettingsPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            // Повертаємось на сторінку входу
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new WelcomePage());
        }
    }
}