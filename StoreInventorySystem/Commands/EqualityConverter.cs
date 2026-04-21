using System;
using System.Globalization;
using System.Windows.Data;

namespace StoreInventorySystem.Commands
{
    /// <summary>
    /// Конвертер для прив'язки RadioButton до рядкової властивості у ViewModel.
    /// Повертає true якщо значення збігається з ConverterParameter.
    /// При зворотньому перетворенні повертає значення параметра якщо RadioButton активний.
    /// </summary>
    public class EqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return parameter?.ToString();
            return Binding.DoNothing;
        }
    }
}
