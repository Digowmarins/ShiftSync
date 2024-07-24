using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using ShiftSync.Application.Dtos;
using ShiftSync.Application.Interfaces;
using ShiftSync.Infrastructure.Data;

namespace ShiftSync.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ShiftContext _context;
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(ShiftContext context, AuthService authService, TokenService tokenService)
        {
            _context = context;
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == loginDto.Email);

            if (employee == null || _authService.ComputeSha256Hash(loginDto.Password) != employee.PasswordHash)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _tokenService.GenerateToken(employee);

            return Ok(new { token });
        }

    }
}
