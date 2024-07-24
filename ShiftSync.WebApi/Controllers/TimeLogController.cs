using AutoMapper;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftSync.Core.Entities;
using ShiftSync.Infrastructure.Data;
using System.Security.Claims;


namespace ShiftSync.API.Controllers
{
    [Route("api/timelog")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly ShiftContext _context;
        private readonly IMapper _mapper;

        public TimeLogController (ShiftContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTimeLogById(int id)
        {
            var timeLog = await _context.TimeLogs.FindAsync(id);

            if (timeLog == null)
            {
                return NotFound();
            }

            return Ok(timeLog);
        }

        [HttpPost("checkin")]
        [Authorize]
        public async Task<ActionResult<TimeLog>> CheckIn()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("ID do usuário inválido.");
            }

            var timeLog = new TimeLog
            {
                EmployeeId = employeeId,
                CheckInTime = DateTime.UtcNow,
                LocationName = "teste",
                
            };

            _context.TimeLogs.Add(timeLog);
            await _context.SaveChangesAsync();

            return Ok(timeLog);
        }

        [HttpPost("checkout")]
        [Authorize]
        public async Task<ActionResult> CheckOut()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("ID do usuário inválido.");
            }

            var timeLog = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.CheckOutTime == null)
                .OrderByDescending(t => t.CheckInTime)
                .FirstOrDefaultAsync();

            if (timeLog == null)
            {
                return NotFound("Nenhum registro de entrada encontrado.");
            }

            timeLog.CheckOutTime = DateTime.UtcNow;
            _context.TimeLogs.Update(timeLog);
            await _context.SaveChangesAsync();

            return Ok(timeLog);
        }

        [HttpPost("breakstart")]
        [Authorize]
        public async Task<ActionResult> BreakStart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("ID do usuário inválido.");
            }

            var timeLog = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.CheckOutTime == null && t.BreakStartTime == null)
                .OrderByDescending(t => t.CheckInTime)
                .FirstOrDefaultAsync();

            if (timeLog == null)
            {
                return NotFound("Nenhum registro de entrada encontrado ou a pausa já foi iniciada.");
            }

            timeLog.BreakStartTime = DateTime.UtcNow;
            _context.TimeLogs.Update(timeLog);
            await _context.SaveChangesAsync();

            return Ok(timeLog);
        }

        [HttpPost("breakend")]
        [Authorize]
        public async Task<ActionResult> BreakEnd()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("ID do usuário inválido.");
            }

            var timeLog = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.CheckOutTime == null && t.BreakStartTime != null && t.BreakEndTime == null)
                .OrderByDescending(t => t.CheckInTime)
                .FirstOrDefaultAsync();

            if (timeLog == null)
            {
                return NotFound("Nenhum registro de entrada encontrado ou a pausa não foi iniciada.");
            }

            timeLog.BreakEndTime = DateTime.UtcNow;
            _context.TimeLogs.Update(timeLog);
            await _context.SaveChangesAsync();

            return Ok(timeLog);
        }

    }
}
