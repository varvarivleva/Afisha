using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AfishaApi.Contracts;
using AfishaApi.Models;
using AfishaApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AfishaApi.Controllers
{
    [Authorize] // Требуется авторизация для всех методов
    [ApiController]
    [Route("api/event_card")]
    public class EventCardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventCardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("show_info/{eventId}")]
        [ProducesResponseType(typeof(EventInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Добавляем 401
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShowInfo(Guid eventId)
        {
            if (eventId == null)
            {
                return BadRequest(new { Message = "No eventId." });
            }
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

        [HttpPost("create_event")]
        [ProducesResponseType(typeof(EventResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Добавляем 401
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpPost("booking")]
        [ProducesResponseType(typeof(BookingResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Добавляем 401
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return NotFound(new { Message = "Event not found or no tickets available." });
            }

            var booking = new BookingEntityDb
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = request.EventId,
                BookingTime = DateTime.Now,
                Status = "success",
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone
            };

            eventInfo.TicketsAvailable -= 1;
            _context.Events.Update(eventInfo);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new BookingResponseDto { Message = "Booking completed successfully." });
        }
    }
}
