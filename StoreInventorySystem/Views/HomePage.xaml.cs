using System.Windows.Controls;
using StoreInventorySystem.ViewModels;

namespace StoreInventorySystem.Views
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            this.DataContext = new HomeViewModel();
        }
    }
}
