using System.Windows;
using System.Windows.Controls;
using StoreInventorySystem.Views;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.Views
{
    public partial class MainDashboardPage : Page
    {
        public MainDashboardPage()
        {
            InitializeComponent();
            // Встановлюємо роль користувача з нашого сервісу авторизації
            UserRoleText.Text = AuthService.CurrentUser?.Role ?? "Користувач";
            // При завантаженні за замовчуванням відкриваємо список товарів
            ContentFrame.Navigate(new InventoryPage());
        }
      
        private void NavHome_Click(object sender, RoutedEventArgs e)
        {
            // Можна створити окрему сторінку статистики HomePage
            MessageBox.Show("Вітаємо в системі обліку товарів!");
        }

        private void NavInventory_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new InventoryPage());
        }

        private void NavAdd_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AddProductPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            // Повертаємось на самий початок (вікно привітання) через NavigationService головного вікна
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new WelcomePage());
        }
        private void NavReports_Click(object sender, RoutedEventArgs e)
        {
            // Показуємо повідомлення, щоб програма не видавала помилку
            MessageBox.Show("Розділ звітів знаходиться в розробці!");
        }
    }
}