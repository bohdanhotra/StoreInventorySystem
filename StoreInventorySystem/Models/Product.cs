using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StoreInventorySystem.Models
{
    /// <summary>
    /// Модель товару на складі. Реалізує INotifyPropertyChanged
    /// для автоматичного оновлення інтерфейсу при зміні властивостей.
    /// </summary>
    public class Product : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _category;
        private decimal _price;
        private int _quantity;
        private string _imagePath;
        private string _description;

        /// <summary>Унікальний ідентифікатор товару.</summary>
        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        /// <summary>Назва товару.</summary>
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        /// <summary>Категорія товару (наприклад, "Електроніка").</summary>
        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        /// <summary>Ціна одиниці товару у гривнях.</summary>
        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        /// <summary>Кількість одиниць на складі.</summary>
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        /// <summary>Шлях до зображення товару на диску.</summary>
        public string ImagePath
        {
            get => _imagePath;
            set { _imagePath = value; OnPropertyChanged(); }
        }

        /// <summary>Текстовий опис товару.</summary>
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}