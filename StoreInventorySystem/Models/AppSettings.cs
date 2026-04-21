namespace StoreInventorySystem.Models
{
    /// <summary>
    /// Налаштування програми, що зберігаються між сеансами.
    /// Серіалізуються у файл settings.json.
    /// </summary>
    public class AppSettings
    {
        /// <summary>Код мови інтерфейсу: "uk" або "en".</summary>
        public string Language { get; set; } = "uk";

        /// <summary>Назва теми оформлення: "Light" або "Dark".</summary>
        public string Theme { get; set; } = "Light";
    }
}
