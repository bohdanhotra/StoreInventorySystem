using System.Globalization;
using System.Windows.Data;

namespace StoreInventorySystem.Commands
{
    // Конвертер для прив'язки RadioButton до рядкової властивості
    // Повертає true якщо значення == параметр
    public class EqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Якщо RadioButton виставлений в true, повертаємо значення параметра
            if (value is bool b && b)
                return parameter?.ToString();
            return Binding.DoNothing;
        }
    }
}
