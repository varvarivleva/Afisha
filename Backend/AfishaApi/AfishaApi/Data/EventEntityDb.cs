public class EventEntityDb
{
    public Guid Id { get; set; }
    public Guid OrganizatorId {  get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EventTime { get; set; }
    public string Venue { get; set; }
    public int TicketsAvailable { get; set; }
    public decimal TicketPrice { get; set; }
}
