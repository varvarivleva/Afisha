namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс для передачи данных, необходимых для хранения информации о событии.
    ///  Строка идентификатора "T:AfishaApi.Contracts.BookingEventDto".
    /// </summary> 
    public class BookingEventDto
    {
        /// <summary>
        /// Поле, хранящее ID события.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingEventDto.Id".
        /// </summary>  
        public Guid Id { get; set; }
        /// <summary>
        /// Поле, хранящее название события.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingEventDto.Title".
        /// </summary>  
        public string Title { get; set; }
        /// <summary>
        /// Поле, хранящее дату и время события.
        /// Строка идентификатора "F:AfishaApi.Contracts.BookingEventDto.EventTime".
        /// </summary>  
        public DateTime EventTime { get; set; }
    }
}
