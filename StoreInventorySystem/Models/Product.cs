using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StoreInventorySystem.Models
{
    public class Product : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private decimal price;
        private int quantity;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}