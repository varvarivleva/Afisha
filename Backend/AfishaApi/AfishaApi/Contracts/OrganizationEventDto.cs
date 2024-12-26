namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс списка организованных событий.
    ///  Строка идентификатора "T:AfishaApi.Contracts.OrganizationEventDto".
    /// </summary> 
    public class OrganizationEventDto
    {
        /// <summary>
        /// Поле, хранящее идентификатор организуемого события.
        /// Строка идентификатора "F:AfishaApi.Contracts.OrganizationEventDto.Id".
        /// </summary>  
        public Guid Id { get; set; }
        /// Поле, хранящее описание события.
        /// Строка идентификатора "F:AfishaApi.Contracts.OrganizationEventDto.Description".
        /// </summary>  
        public string Title { get; set; }
        /// <summary>
        /// Поле, хранящее дату и время события.
        /// Строка идентификатора "F:AfishaApi.Contracts.OrganizationEventDto.EventTime".
        /// </summary> 
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Поле, хранящее доступность события.
        /// Строка идентификатора "F:AfishaApi.Contracts.OrganizationEventDto.TicketsAvailable".
        /// </summary> 
        public int TicketsAvailable { get; set; }
    }
}
