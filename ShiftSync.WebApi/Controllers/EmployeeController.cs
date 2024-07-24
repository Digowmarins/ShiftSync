using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftSync.Application.Dtos;
using ShiftSync.Application.Interfaces;
using ShiftSync.Core.Entities;
using ShiftSync.Infrastructure.Data;

namespace ShiftSync.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ShiftContext _context;
        private readonly IMapper _mapper;
        private readonly AuthService _authService;

        public EmployeeController(ShiftContext context, IMapper mapper, AuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
    }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var passwordHash = _authService.ComputeSha256Hash(createEmployeeDto.Password);
                Employee employee = _mapper.Map<Employee>(createEmployeeDto);
                employee.PasswordHash = passwordHash;
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                var readEmployeeDto = _mapper.Map<ReadEmployeeDto>(employee);

                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, readEmployeeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee != null)
                {
                    var readEmployeeDto = _mapper.Map<ReadEmployeeDto>(employee);
                    return Ok(readEmployeeDto);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                var readEmployeeDtos = _mapper.Map<List<ReadEmployeeDto>>(employees);
                return Ok(readEmployeeDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}