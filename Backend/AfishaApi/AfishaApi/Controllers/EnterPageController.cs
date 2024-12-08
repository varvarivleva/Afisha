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
    [Route("api/enter_page")]
    public class EnterPageController : ControllerBase
    {
        private readonly AppDbContext _context;  //��� ������ � ��

        public EnterPageController(AppDbContext context) //�����������
        {
            _context = context;
        }

        [HttpPost("register")]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto? request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "All fields are required" });
            }

            using (_context)
            {
                // �������� �� ������������� ������������
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (existingUser != null)
                {
                    return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "User already exists" });
                }

                // �������� ������ ������������
                var newUser = new UserEntityDb
                {
                    Username = request.Username,
                    Password = request.Password, // �� �������� ������������ ����������� ������
                    Email = request.Email
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }

            return Ok(new EnterPageResponseDto { StatusCode = StatusCodes.Status200OK, Message = "User created" });
        }


        [HttpPost("authorizaton")]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<EnterPageResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthAsync([FromBody] AuthorizationRequestDto? request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new EnterPageResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "All field are required" });
            }

            //��� ����� �� ���������� ������������ �� ��

            //�������� ������ ����������� ������, ������� ����� ������������ �����

            return Ok(new EnterPageResponseDto { StatusCode = StatusCodes.Status200OK, Message = "Access granted" });

        }
    }
}
