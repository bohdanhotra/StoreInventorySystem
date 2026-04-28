using System.Windows;

namespace StoreInventorySystem
{
    /// <summary>
    /// Точка входу застосунку. Відповідає за завантаження ресурсів,
    /// перемикання теми та мови інтерфейсу.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Застосовує тему оформлення, замінюючи перший словник ресурсів.
        /// </summary>
        /// <param name="themeName">"Light" або "Dark".</param>
        public static void ApplyTheme(string themeName)
        {
            var dict = new ResourceDictionary();
            if (themeName == "Dark")
                dict.Source = new Uri("Resources/DarkTheme.xaml", UriKind.Relative);
            else
                dict.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);

            Current.Resources.MergedDictionaries[0] = dict;
        }

        /// <summary>
        /// Застосовує мову інтерфейсу, замінюючи другий словник ресурсів.
        /// </summary>
        /// <param name="langCode">"uk" або "en".</param>
        public static void ApplyLanguage(string langCode)
        {
            var dict = new ResourceDictionary();
            if (langCode == "en")
                dict.Source = new Uri("Resources/Strings.en.xaml", UriKind.Relative);
            else
                dict.Source = new Uri("Resources/Strings.uk.xaml", UriKind.Relative);

            Current.Resources.MergedDictionaries[1] = dict;
        }
    }
}
