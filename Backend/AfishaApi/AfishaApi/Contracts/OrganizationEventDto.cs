namespace AfishaApi.Contracts
{
    public class OrganizationEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EventTime { get; set; }
        public int TicketsAvailable { get; set; }
    }
}
