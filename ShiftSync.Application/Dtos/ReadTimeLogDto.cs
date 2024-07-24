namespace ShiftSync.Application.Dtos
{
    public class ReadTimeLogDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? BreakStartTime { get; set; }
        public DateTime? BreakEndTime { get; set; }
        public string LocationName { get; set; }
    }
}
