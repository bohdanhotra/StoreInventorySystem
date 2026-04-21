using System.Windows.Controls;
using StoreInventorySystem.ViewModels;

namespace StoreInventorySystem.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();
        }
    }
}
