using System.Windows.Controls;
using StoreInventorySystem.ViewModels;

namespace StoreInventorySystem.Views
{
    public partial class AddProductPage : Page
    {
        public AddProductPage()
        {
            InitializeComponent();
            // Підключаємо ViewModel до View
            this.DataContext = new AddProductViewModel();
        }
    }
}
