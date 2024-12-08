public class BookingEntityDb
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public DateTime BookingTime { get; set; }
    public string Status { get; set; } // success или canceled
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}
