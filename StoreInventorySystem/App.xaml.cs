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
           
        }

        /// <summary>
        /// Застосовує мову інтерфейсу, замінюючи другий словник ресурсів.
        /// </summary>
        /// <param name="langCode">"uk" або "en".</param>
        public static void ApplyLanguage(string langCode)
        {
            
        }
    }
}
