namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс кэша для передачи данных, необходимых для аутентификации пользователя.
    ///  Строка идентификатора "T:AfishaApi.Contracts.AuthorizationRequestDto".
    /// </summary> 
    public class AuthorizationRequestDto
    {
        /// <summary>
        /// Поле, хранящее логин пользователя.
        /// Строка идентификатора "F:AfishaApi.Contracts.AuthorizationRequestDto.Username".
        /// </summary>  
        public string Username { get; set; }
        /// <summary>
        /// Поле, хранящее пароль пользователя.
        /// Строка идентификатора "F:AfishaApi.Contracts.AuthorizationRequestDto.Username".
        /// </summary>  
        public string Password { get; set; }
    }
}

