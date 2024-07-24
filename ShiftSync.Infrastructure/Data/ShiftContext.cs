using Microsoft.EntityFrameworkCore;
using ShiftSync.Core.Entities;

namespace ShiftSync.Infrastructure.Data
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }    

    }
}
