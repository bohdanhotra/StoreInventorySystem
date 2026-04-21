using System.Windows;

namespace StoreInventorySystem
{
    public partial class App : Application
    {
        // Перемикання теми
        public static void ApplyTheme(string themeName)
        {
            var dict = new ResourceDictionary();
            if (themeName == "Dark")
                dict.Source = new Uri("Resources/DarkTheme.xaml", UriKind.Relative);
            else
                dict.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);

            // Замінюємо перший словник (тему) у MergedDictionaries
            var merged = Current.Resources.MergedDictionaries;
            merged[0] = dict;
        }

        // Перемикання мови
        public static void ApplyLanguage(string langCode)
        {
            var dict = new ResourceDictionary();
            if (langCode == "en")
                dict.Source = new Uri("Resources/Strings.en.xaml", UriKind.Relative);
            else
                dict.Source = new Uri("Resources/Strings.uk.xaml", UriKind.Relative);

            // Замінюємо другий словник (локалізацію) у MergedDictionaries
            var merged = Current.Resources.MergedDictionaries;
            merged[1] = dict;
        }
    }
}
