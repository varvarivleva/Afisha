
namespace AfishaApi.Contracts
{
    public class ShowAllEventsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set; }
        public string Venue { get; set; }
    }
}