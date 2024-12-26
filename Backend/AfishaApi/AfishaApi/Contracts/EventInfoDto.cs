namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс информации о событии.
    ///  Строка идентификатора "T:AfishaApi.Contracts.EventInfoDto".
    /// </summary> 
    public class EventInfoDto
    {
        /// <summary>
        /// Поле, хранящее название события.
        /// Строка идентификатора "F:AfishaApi.Contracts.EventInfoDto.Title".
        /// </summary>  
        public string Title { get; set; }
        /// Поле, хранящее описание события.
        /// Строка идентификатора "F:AfishaApi.Contracts.EventInfoDto.Description".
        /// </summary>  
        public string Description { get; set; }
        /// <summary>
        /// Поле, хранящее дату и время события.
        /// Строка идентификатора "F:AfishaApi.Contracts.EventInfoDto.EventTime".
        /// </summary> 
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Поле, хранящее место проведения события.
        /// Строка идентификатора "F:AfishaApi.Contracts.EventInfoDto.Venue".
        /// </summary>  
        public string Venue { get; set; }
        /// <summary>
        /// Поле, хранящее доступность события.
        /// Строка идентификатора "F:AfishaApi.Contracts.EventInfoDto.TicketsAvailable".
        /// </summary> 
        public int TicketsAvailable { get; set; }
        /// <summary>
        /// Поле, хранящее название события.
        /// Строка идентификатора "F:AfishaApi.Contracts.EventInfoDto.TicketPrice".
        /// </summary>  
        public decimal TicketPrice { get; set; }
    }
}
