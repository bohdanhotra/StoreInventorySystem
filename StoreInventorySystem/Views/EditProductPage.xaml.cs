using System.Windows.Controls;
using StoreInventorySystem.Models;
using StoreInventorySystem.ViewModels;

namespace StoreInventorySystem.Views
{
    public partial class EditProductPage : Page
    {
        public EditProductPage(Product productToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditProductViewModel(productToEdit);
        }
    }
}
