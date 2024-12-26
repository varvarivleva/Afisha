using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AfishaApi.Contracts;
using AfishaApi.Models;
using AfishaApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AfishaApi.Controllers
{
    /// <summary>
    ///  Класс для регистрации и авторизации пользователей в приложении.
    ///  Строка идентификатора "T:AfishaApi.Controllers.EnterPageController".
    /// </summary> 
    [ApiController]
    [Route("api/enter_page")]
    public class EnterPageController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Метод для вычисления.
        /// Строка идентификатора "M:AfishaApi.Controllers.EnterPageController.EnterPageController".
        /// </summary>
        public EnterPageController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод для обработки запросов на регистрацию.
        /// Строка идентификатора "M:AfishaApi.Controllers.EnterPageController.RegisterAsync".
        /// </summary>
        /// <param name="RegisterRequestDto">Запрос из контракта
        /// .</param>
        /// <returns>Значение, равное коду запроса.</returns>
        [HttpPost("register")]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto? request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Password) ||
                    string.IsNullOrWhiteSpace(request.Email))
                {
                    return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "All fields are required" });
                }

                using (_context)
                {
                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.Username == request.Username);

                    if (existingUser != null)
                    {
                        return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "User already exists" });
                    }

                    var newUser = new UserEntityDb
                    {
                        Id = Guid.NewGuid(),
                        Username = request.Username,
                        Password = request.Password,
                        Email = request.Email
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                }

                return Ok(new EnterPageResponseDto { StatusCode = StatusCodes.Status200OK, Message = "User created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Метод для обработки запросов на авторизацию.
        /// Строка идентификатора "M:AfishaApi.Controllers.EnterPageController.AuthAsync".
        /// </summary>
        /// <param name="AuthorizationRequestDto">Запрос из контракта
        /// .</param>
        /// <returns>Значение, равное коду запроса, и токен.</returns>
        [HttpPost("authorization")]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthAsync([FromBody] AuthorizationRequestDto? request)
        {
                if (string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "All fields are required" });
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "Invalid username" });
                }
                if (request.Password != user.Password)
                {
                    return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "Invalid password" });
                }

                var token = GenerateToken(user);

                return Ok(new EnterPageResponseDto { StatusCode = StatusCodes.Status200OK, Message = "Access granted", Token = token });
        }
        /// <summary>
        /// Метод для генерации токенов.
        /// Строка идентификатора "M:AfishaApi.Controllers.EnterPageController.GenerateToken".
        /// </summary>
        /// <param name="user">Запрос из базы данных.
        /// .</param>
        /// <returns>Токен.</returns>
        private string GenerateToken(UserEntityDb user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("32_characters_long_super_secret_key"));
            if (key.KeySize < 256)
            {
                throw new ArgumentException("Key length must be at least 256 bits (32 characters).");
            }

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
