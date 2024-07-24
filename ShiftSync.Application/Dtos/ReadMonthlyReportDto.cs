namespace ShiftSync.Application.Dtos
{
    public class ReadMonthlyReportDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int TotalHoursWorked { get; set; }
        public int TotalOvertimeHours { get; set; }
        public int TotalBreakHours { get; set; }
    }
}
