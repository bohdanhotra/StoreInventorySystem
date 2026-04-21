using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StoreInventorySystem.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _category;
        private decimal _price;
        private int _quantity;
        private string _imagePath;
        private string _description;

        public string Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Category { get => _category; set { _category = value; OnPropertyChanged(); } }
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged(); } }
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        public string ImagePath { get => _imagePath; set { _imagePath = value; OnPropertyChanged(); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}