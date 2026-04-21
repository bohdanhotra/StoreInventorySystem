using System.Windows;
using StoreInventorySystem.Views;

namespace StoreInventorySystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Views.MainDashboardPage()); 
        }

        public void NavigateToHome()
        {
            MainFrame.Navigate(new Views.InventoryPage());
        }
    }
}