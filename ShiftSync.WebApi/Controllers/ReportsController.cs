using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftSync.Application.Dtos;
using ShiftSync.Application.Interfaces;
using ShiftSync.Application.Services;
using ShiftSync.Infrastructure.Data;
using System.Security.Claims;

namespace ShiftSync.API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ShiftContext _context;
        private readonly IMapper _mapper;
        private readonly AuthService _authService;
        private readonly EmailSenderService _emailSenderService;
        public ReportsController(ShiftContext context, IMapper mapper, AuthService authService, EmailSenderService emailSenderService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _emailSenderService = emailSenderService;
        }

        [HttpPost("monthly")]
        [Authorize]
        public async Task<ActionResult<ReadMonthlyReportDto>> GetMonthlyReport([FromBody] MonthlyReportRequestDto requestDto)
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

            // Filtrar os registros de tempo do usuário logado
            var report = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.CheckInTime.Month == requestDto.Month && t.CheckInTime.Year == requestDto.Year)
                .GroupBy(t => t.EmployeeId)
                .Select(g => new ReadMonthlyReportDto
                {
                    EmployeeId = g.Key,
                    EmployeeName = _context.Employees.FirstOrDefault(e => e.Id == g.Key).Name,
                    TotalHoursWorked = g.Sum(t => (int?)EF.Functions.DateDiffMinute(t.CheckInTime, t.CheckOutTime) ?? 0) / 60,
                    TotalOvertimeHours = g.Where(t => t.CheckOutTime > t.CheckInTime.AddHours(8))
                                          .Sum(t => (int?)EF.Functions.DateDiffMinute(t.CheckInTime.AddHours(8), t.CheckOutTime) ?? 0) / 60,
                    TotalBreakHours = g.Sum(t => (int?)EF.Functions.DateDiffMinute(t.BreakStartTime, t.BreakEndTime) ?? 0) / 60
                })
                .FirstOrDefaultAsync();

            if (report == null)
            {
                return NotFound("Nenhum registro de tempo encontrado para o usuário no período especificado.");
            }

            var email = User.FindFirstValue(ClaimTypes.Email);
            var subject = "Relatório Mensal";
            var message = $"Olá {report.EmployeeName},\n\nAqui está o seu relatório mensal:\n\n" +
                          $"Total de Horas Trabalhadas: {report.TotalHoursWorked}\n" +
                          $"Total de Horas Extras: {report.TotalOvertimeHours}\n" +
                          $"Total de Horas de Pausa: {report.TotalBreakHours}\n\n" +
                          "Atenciosamente,\nShiftSync Team";

            try
            {
                await _emailSenderService.SendEmailAsync(email, subject, message, report.EmployeeName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao enviar o e-mail.");
            }

            return Ok(report);
        }
    }
}
