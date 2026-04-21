using System.Windows.Controls;
using StoreInventorySystem.ViewModels;

namespace StoreInventorySystem.Views
{
    public partial class InventoryPage : Page
    {
        public InventoryPage()
        {
            InitializeComponent();
            // Підключаємо ViewModel до View
            this.DataContext = new InventoryViewModel();
        }
    }
}