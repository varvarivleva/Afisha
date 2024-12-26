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
    /// <summary>
    ///  Класс для управления карточкой события.
    ///  Строка идентификатора "T:AfishaApi.Controllers.EventCardController".
    /// </summary> 
    [Authorize]
    [ApiController]
    [Route("api/event_card")]
    public class EventCardController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Метод для вычисления.
        /// Строка идентификатора "M:AfishaApi.Controllers.EventCardController.EventCardController".
        /// </summary>
        public EventCardController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для вывода информации карточки события.
        /// Строка идентификатора "M:AfishaApi.Controllers.EventCardController.ShowInfo".
        /// </summary>
        /// <param name="eventId">Идентификатор события.
        /// .</param>
        /// <returns>Значение, равное коду статуса.</returns>
        [HttpGet("show_info/{eventId}")]
        [ProducesResponseType<EventInfoDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShowInfo(Guid eventId)
        {
            try
            {
                var organizatorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (organizatorIdClaim == null || !Guid.TryParse(organizatorIdClaim.Value, out var organizatorId))
                {
                    return Unauthorized(new BaseResponseDto { StatusCode = StatusCodes.Status401Unauthorized, Message = "Invalid auth token." });
                }

                if (eventId == null)
                {
                    return BadRequest(new BaseResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "Enter valid EventId." });
                }

                var eventInfo = await _context.Events.FindAsync(eventId);
                if (eventInfo == null)
                {
                    return NotFound(new BaseResponseDto { StatusCode=StatusCodes.Status404NotFound, Message = "Event not found." });
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Метод для создания события.
        /// Строка идентификатора "M:AfishaApi.Controllers.EventCardController.CreateEvent".
        /// </summary>
        /// <param name="CreateEventDto">Запрос контракта.
        /// .</param>
        /// <returns>Значение, равное коду статуса.</returns>
        [HttpPost("create_event")]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto request)
        {
            try
            {
                var organizatorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (organizatorIdClaim == null || !Guid.TryParse(organizatorIdClaim.Value, out var organizatorId))
                {
                    return Unauthorized(new BaseResponseDto { StatusCode = StatusCodes.Status401Unauthorized, Message = "Invalid auth token." });
                }

                if (string.IsNullOrWhiteSpace(request.Title) ||
                    string.IsNullOrWhiteSpace(request.Description) ||
                    request.EventTime == DateTime.MinValue ||
                    string.IsNullOrWhiteSpace(request.Venue) ||
                    request.TicketsAvailable <= 0 ||
                    request.TicketPrice<=0)
                {
                    return BadRequest(new BaseResponseDto { StatusCode=StatusCodes.Status400BadRequest, Message = "All fields are required." });
                }

                var newEvent = new EventEntityDb
                {
                    Id = Guid.NewGuid(),
                    OrganizatorId = organizatorId,
                    Title = request.Title,
                    Description = request.Description,
                    EventTime = request.EventTime,
                    Venue = request.Venue,
                    TicketsAvailable = request.TicketsAvailable,
                    TicketPrice = request.TicketPrice
                };

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return Ok(new BaseResponseDto { StatusCode=StatusCodes.Status200OK, Message = "Event created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Метод для создания бронирования.
        /// Строка идентификатора "M:AfishaApi.Controllers.EventCardController.Booking".
        /// </summary>
        /// <param name="BookingRequestDto">Запрос контракта.
        /// .</param>
        /// <returns>Значение, равное коду статуса.</returns>
        [HttpPost("booking")]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Booking([FromBody] BookingRequestDto request)
        {
            try
            {
                var uesrIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (uesrIdClaim == null || !Guid.TryParse(uesrIdClaim.Value, out var userId))
                {
                    return Unauthorized(new BaseResponseDto { StatusCode = StatusCodes.Status401Unauthorized, Message = "Invalid auth token." });
                }

                if (string.IsNullOrWhiteSpace(request.FirstName) ||
                    string.IsNullOrWhiteSpace(request.LastName) ||
                    string.IsNullOrWhiteSpace(request.Phone) ||
                    request.EventId == Guid.Empty)
                {
                    return BadRequest(new BaseResponseDto { StatusCode=StatusCodes.Status400BadRequest, Message = "All fields are required." });
                }

                var eventInfo = await _context.Events.FindAsync(request.EventId);
                if (eventInfo == null || eventInfo.TicketsAvailable <= 0)
                {
                    return NotFound(new BaseResponseDto { StatusCode=StatusCodes.Status404NotFound, Message = "Event not found or no tickets available." });
                }

                var booking = new BookingEntityDb
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    EventId = request.EventId,
                    BookingTime = DateTime.UtcNow,
                    Status = "success",
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Phone = request.Phone
                };

                eventInfo.TicketsAvailable -= 1;
                _context.Events.Update(eventInfo);
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return Ok(new BaseResponseDto { StatusCode=StatusCodes.Status200OK, Message = "Booking completed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
