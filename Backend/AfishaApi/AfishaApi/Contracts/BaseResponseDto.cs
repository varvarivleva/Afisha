namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс кэша для передачи данных, необходимых для аутентификации пользователя.
    ///  Строка идентификатора "T:AfishaApi.Contracts.BaseResponseDto".
    /// </summary> 
    public class BaseResponseDto
    {
        /// <summary>
        /// Поле, хранящее код состояния ответа.
        /// Строка идентификатора "F:AfishaApi.Contracts.BaseResponseDto.StatusCode".
        /// </summary>  
        public int StatusCode { get; set; }
        /// <summary>
        /// Поле, хранящее информацию о результате операции.
        /// Строка идентификатора "F:AfishaApi.Contracts.BaseResponseDto.Message".
        /// </summary>  
        public string Message { get; set; }
    }
}
