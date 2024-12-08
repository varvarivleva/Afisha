using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AfishaApi.Contracts;
using AfishaApi.Models;
using AfishaApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AfishaApi.Controllers
{
    [ApiController]
    [Route("api/event_card")]
    public class EventCardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventCardController(AppDbContext context)
        {
            _context = context;
        }

        // Показ карточки события
        [HttpGet("show_info/{eventId}")]
        [ProducesResponseType<EventInfoDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ShowInfo(Guid eventId)
        {
            var eventInfo = await _context.Events.FindAsync(eventId);
            if (eventInfo == null)
            {
                return NotFound(new { Message = "Event not found." });
            }
            return Ok(new EventInfoDto
            {
                Title = eventInfo.Title,
                Description = eventInfo.Description,
                EventTime = eventInfo.EventTime,
                Venue = eventInfo.Venue,
                TicketsAvailable = eventInfo.TicketsAvailable,
                TicketPrice = eventInfo.TicketPrice
            });
        }

        // Создание карточки события
        [HttpPost("create_event")]
        [ProducesResponseType<EventResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<EventResponseDto>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) ||
                string.IsNullOrWhiteSpace(request.Description) ||
                request.EventTime == DateTime.MinValue ||
                string.IsNullOrWhiteSpace(request.Venue) ||
                request.TicketsAvailable <= 0 ||
                request.TicketPrice <= 0)
            {
                return BadRequest(new { Message = "All fields are required." });
            }

            var newEvent = new EventEntityDb
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                EventTime = request.EventTime,
                Venue = request.Venue,
                TicketsAvailable = request.TicketsAvailable,
                TicketPrice = request.TicketPrice
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return Ok(new EventResponseDto { Message = "Event created successfully." });
        }

        // Бронирование места на событие
        [HttpPost("booking")]
        [ProducesResponseType<BookingResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<BookingResponseDto>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Booking([FromBody] BookingRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Phone) ||
                request.EventId == Guid.Empty)
            {
                return BadRequest(new { Message = "All fields are required." });
            }

            var eventInfo = await _context.Events.FindAsync(request.EventId);
            if (eventInfo == null || eventInfo.TicketsAvailable <= 0)
            {
                return BadRequest(new { Message = "Event not found or no tickets available." });
            }

            var booking = new BookingEntityDb
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId, // Предполагается, что UserId передается в запросе
                EventId = request.EventId,
                BookingTime = DateTime.Now,
                Status = "success",
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone
            };

            eventInfo.TicketsAvailable -= 1; // Уменьшаем количество доступных мест
            _context.Events.Update(eventInfo);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new BookingResponseDto { Message = "Booking completed successfully." });
        }
    }
}
