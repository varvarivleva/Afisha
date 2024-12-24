using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AfishaApi.Models;
using AfishaApi.Data;
using AfishaApi.Contracts;

namespace AfishaApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/my_events")]
    public class MyEventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MyEventsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список мероприятий, организованных текущим пользователем.
        /// </summary>
        [HttpGet("organization")]
        [ProducesResponseType<IEnumerable<OrganizationEventDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizedEvents()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new BaseResponseDto { StatusCode=StatusCodes.Status401Unauthorized, Message = "Invalid auth token." });
                }

                var userId = Guid.Parse(userIdClaim.Value);

                var events = await _context.Events
                    .Where(e => e.OrganizatorId == userId) 
                    .Select(e => new OrganizationEventDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        EventTime = e.EventTime,
                        TicketsAvailable = e.TicketsAvailable
                    })
                    .ToListAsync();

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Получить список мероприятий, которые забронированы текущим пользователем.
        /// </summary>
        [HttpGet("bookings")]
        [ProducesResponseType<IEnumerable<BookingEventDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new BaseResponseDto { StatusCode = StatusCodes.Status401Unauthorized, Message = "Invalid auth token." });
                }

                var userId = Guid.Parse(userIdClaim.Value);

                var bookings = await _context.Bookings
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Event)
                    .Select(b => new BookingEventDto
                    {
                        Id = b.EventId,
                        Title = b.Event.Title,
                        EventTime = b.Event.EventTime
                    })
                    .ToListAsync();

                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
