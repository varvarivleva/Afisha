namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс создания события.
    ///  Строка идентификатора "T:AfishaApi.Contracts.CreateEventDto".
    /// </summary> 
    public class CreateEventDto
    {
        /// <summary>
        /// Поле, хранящее название события.
        /// Строка идентификатора "F:AfishaApi.Contracts.CreateEventDto.Title".
        /// </summary>  
        public string Title { get; set; }
        /// <summary>
        /// Поле, хранящее описание события.
        /// Строка идентификатора "F:AfishaApi.Contracts.CreateEventDto.Description".
        /// </summary>  
        public string Description { get; set; }
        /// <summary>
        /// Поле, хранящее дату и время события.
        /// Строка идентификатора "F:AfishaApi.Contracts.CreateEventDto.EventTime".
        /// </summary>  
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Поле, хранящее место проведения события.
        /// Строка идентификатора "F:AfishaApi.Contracts.CreateEventDto.Venue".
        /// </summary>  
        public string Venue { get; set; }
        /// <summary>
        /// Поле, хранящее доступность события.
        /// Строка идентификатора "F:AfishaApi.Contracts.CreateEventDto.TicketsAvailable".
        /// </summary>  
        public int TicketsAvailable { get; set; }
        /// <summary>
        /// Поле, хранящее название события.
        /// Строка идентификатора "F:AfishaApi.Contracts.CreateEventDto.TicketPrice".
        /// </summary>  
        public decimal TicketPrice { get; set; }
    }
}
