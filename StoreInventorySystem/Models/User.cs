namespace StoreInventorySystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // У реальних проєктах тут має бути Hash
        public string Role { get; set; }     // "Admin" або "Manager"
    }
}