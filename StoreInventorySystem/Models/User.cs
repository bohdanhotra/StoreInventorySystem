namespace StoreInventorySystem.Models
{
    /// <summary>
    /// Модель користувача системи.
    /// Пароль зберігається у вигляді SHA-256 хешу — не у відкритому тексті.
    /// </summary>
    public class User
    {
        /// <summary>Порядковий номер користувача.</summary>
        public int Id { get; set; }

        /// <summary>Логін (унікальне ім'я) користувача.</summary>
        public string Username { get; set; }

        /// <summary>SHA-256 хеш пароля.</summary>
        public string Password { get; set; }

        /// <summary>Роль користувача: "Admin" або "Manager".</summary>
        public string Role { get; set; }
    }
}