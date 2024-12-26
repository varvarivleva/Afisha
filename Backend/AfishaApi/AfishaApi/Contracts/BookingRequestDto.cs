namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс создания бронирования.
    ///  Строка идентификатора "T:AfishaApi.Contracts.BookingRequestDto".
    /// </summary> 
    public class BookingRequestDto
    {
        /// <summary>
        /// Поле, хранящее ID события.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingRequestDto.EventId".
        /// </summary>  
        public Guid EventId { get; set; }
        /// <summary>
        /// Поле, хранящее имя пользователя, регистрируемого на событие.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingRequestDto.FirstName".
        /// </summary>  
        public string FirstName { get; set; }
        /// <summary>
        /// Поле, хранящее фамилию пользователя, регистрируемого на событие.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingRequestDto.LastName".
        /// </summary>  
        public string LastName { get; set; }
        /// <summary>
        /// Поле, хранящее телефон пользователя, регистрируемого на событие.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingRequestDto.Phone".
        /// </summary>  
        public string Phone { get; set; }
    }
}
