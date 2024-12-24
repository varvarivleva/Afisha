namespace AfishaApi.Contracts
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set; }
        public string Venue { get; set; }
        public int TicketsAvailable { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
