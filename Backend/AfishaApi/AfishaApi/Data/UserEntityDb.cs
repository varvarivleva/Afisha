namespace AfishaApi.Data
{
    public class UserEntityDb
    {
        //[Key]
        public Guid Id { get; set; } // Первичный ключ
        public string Username { get; set; }
        public string Password { get; set; } // Лучше использовать хэш пароля
        public string Email { get; set; }
    }
}
