﻿namespace AfishaApi.Contracts
{
    public class BookingRequestDto
    {
        public Guid EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
