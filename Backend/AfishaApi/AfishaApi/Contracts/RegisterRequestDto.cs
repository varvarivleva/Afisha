namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс списка забронированных событий.
    ///  Строка идентификатора "T:AfishaApi.Contracts.RegisterRequestDto".
    /// </summary> 
    public class RegisterRequestDto
    {
        /// <summary>
        /// Поле, хранящее логин пользователя.
        /// Строка идентификатора "F:AfishaApi.Contracts.RegisterRequestDto.Username".
        /// </summary>  
        public string Username { get; set; }
        /// <summary>
        /// Поле, хранящее пароль пользователя.
        /// Строка идентификатора "F:AfishaApi.Contracts.RegisterRequestDto.Password".
        /// </summary>  
        public string Password { get; set; }
        /// <summary>
        /// Поле, хранящее электронную почту пользователя.
        /// Строка идентификатора "F:AfishaApi.Contracts.RegisterRequestDto.Email".
        /// </summary>  
        public string Email { get; set; }
    }
}
