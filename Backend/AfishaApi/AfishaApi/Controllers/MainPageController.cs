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

namespace AfishaApi.Controllers
{
    //[Authorize] // Требуется авторизация для всех методов
    [ApiController]
    [Route("api/main_page")]
    public class MainPageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MainPageController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить весь список не прошедших мероприятий.
        /// </summary>
        [HttpGet("show_events")]
        [ProducesResponseType(typeof(IEnumerable<ShowAllEventsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                //var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                //if (userIdClaim == null)
                //{
                //    return Unauthorized(new { Message = "User ID not found in token." });
                //}

                // Получить текущую дату и время
                var currentDateTime = DateTime.UtcNow;

                // Запрос к базе данных с применением условий
                var events = await _context.Events
                    .Where(e => e.EventTime > currentDateTime && e.TicketsAvailable > 0)
                    .Select(e => new ShowAllEventsDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Description = e.Description,
                        EventTime = e.EventTime,
                        Venue = e.Venue
                    })
                    .ToListAsync();

                return Ok(new { events });
            }
            catch (Exception ex)
            {
                // Логирование ошибки можно добавить здесь
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
