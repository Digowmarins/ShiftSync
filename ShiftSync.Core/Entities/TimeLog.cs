namespace ShiftSync.Core.Entities
{
    public class TimeLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? BreakStartTime { get; set; }
        public DateTime? BreakEndTime { get; set; }
        public string LocationName { get; set; }
        public Employee Employee { get; set; }

    }
}
