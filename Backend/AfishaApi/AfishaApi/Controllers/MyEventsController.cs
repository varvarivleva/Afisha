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
    [Authorize] // Требуется авторизация для всех методов
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
        [ProducesResponseType(typeof(IEnumerable<OrganizationEventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizedEvents()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "User ID not found in token." });
            }

            var userId = Guid.Parse(userIdClaim.Value); // Получаем ID пользователя из токена

            var events = await _context.Events
                .Where(e => e.OrganizatorId == userId) // Предполагается, что в модели есть поле OrganizerId
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

        /// <summary>
        /// Получить список мероприятий, которые забронированы текущим пользователем.
        /// </summary>
        [HttpGet("bookings")]
        [ProducesResponseType(typeof(IEnumerable<BookingEventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookings()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "User ID not found in token." });
            }

            var userId = Guid.Parse(userIdClaim.Value);// Получаем ID пользователя из токена

            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Event) // Предполагается, что у вас есть навигационное свойство Event в классе BookingEntityDb.
                .Select(b => new BookingEventDto
                {
                    Id = b.EventId, // Получаем ID события из бронирования
                    Title = b.Event.Title,
                    EventTime = b.Event.EventTime
                })
                .ToListAsync();

            return Ok(bookings);
        }
    }
}
