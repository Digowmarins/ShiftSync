using System.ComponentModel.DataAnnotations;

namespace ShiftSync.Application.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        [StringLength(100, ErrorMessage = "O email não pode ter mais que 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O papel é obrigatório.")]
        [StringLength(50, ErrorMessage = "O papel não pode ter mais que 50 caracteres.")]
        public string Role { get; set; }
    }
}
