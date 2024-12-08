public class BookingRequestDto
{
    public Guid UserId { get; set; } // Передайте ID текущего пользователя
    public Guid EventId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}
