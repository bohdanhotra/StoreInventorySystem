namespace StoreInventorySystem.Models
{
    // Клас для зберігання налаштувань програми
    public class AppSettings
    {
        public string Language { get; set; } = "uk";   // "uk" або "en"
        public string Theme { get; set; } = "Light";   // "Light" або "Dark"
    }
}
