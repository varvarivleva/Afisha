namespace AfishaApi.Contracts
{
    public class BookingEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EventTime { get; set; }
    }
}
