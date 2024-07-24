using System.ComponentModel.DataAnnotations;

namespace ShiftSync.Application.Dtos
{
    public class CreateTimeLogDto
    {
        [Required(ErrorMessage = "O horário de entrada é obrigatório.")]
        public DateTime CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public DateTime? BreakStartTime { get; set; }

        public DateTime? BreakEndTime { get; set; }

        [Required(ErrorMessage = "O nome do local é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do local não pode ter mais que 100 caracteres.")]
        public string LocationName { get; set; }
    }
}
