using System.Windows;
using System.Windows.Controls;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.Views
{
    /// <summary>
    /// Головна панель керування з бічним меню навігації.
    /// Містить Frame для відображення піднавігаційних сторінок.
    /// </summary>
    public partial class MainDashboardPage : Page
    {
        public MainDashboardPage()
        {
            InitializeComponent();
            UserNameText.Text = AuthService.CurrentUser?.Username ?? "Користувач";
            UserRoleText.Text = AuthService.CurrentUser?.Role ?? "";
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

        private void NavAbout_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AboutPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new WelcomePage());
        }
    }
}