﻿using System;
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
    /// <summary>
    ///  Класс для списка предстоящих событий.
    ///  Строка идентификатора "T:AfishaApi.Controllers.MainPageController".
    /// </summary> 
    [Authorize]
    [ApiController]
    [Route("api/main_page")]
    public class MainPageController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Метод для вычисления.
        /// Строка идентификатора "M:AfishaApi.Controllers.EnterPageController.MainPageController".
        /// </summary>
        public MainPageController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для вывода непрошедших событий.
        /// Строка идентификатора "M:AfishaApi.Controllers.MainPageController.GetAllEvents".
        /// </summary>
        /// <returns>Значение, равное коду статуса.</returns>
        [HttpGet("show_events")]
        [ProducesResponseType<IEnumerable<ShowAllEventsDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<BaseResponseDto>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new BaseResponseDto {  StatusCode=StatusCodes.Status401Unauthorized, Message = "Invalid auth token." });
                }

                var currentDateTime = DateTime.UtcNow;

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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
