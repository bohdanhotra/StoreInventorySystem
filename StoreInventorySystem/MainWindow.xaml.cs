using System.Windows;
using StoreInventorySystem.Views;

namespace StoreInventorySystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // При старті програми завантажуємо сторінку логіну
            MainFrame.Navigate(new LoginPage());
        }

        // Цей метод викликається з LoginViewModel після успішного входу
        public void NavigateToHome()
        {
            // Тут ми перейдемо на Dashboard (який ми створимо в наступному кроці)
            // MainFrame.Navigate(new MainDashboardPage());
            MessageBox.Show("Успішний вхід! Перехід на HomePage...");
        }
    }
}