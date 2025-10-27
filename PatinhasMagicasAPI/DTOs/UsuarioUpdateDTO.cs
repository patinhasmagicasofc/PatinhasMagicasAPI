using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class UsuarioUpdateDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [Display(Name = "E-mail")]
        [StringLength(50, ErrorMessage = "O e-mail deve ter no máximo 50 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O DDD é obrigatório.")]
        [Range(11, 99, ErrorMessage = "O DDD deve ser um número de 2 dígitos (entre 11 e 99).")]
        public int Ddd { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "O telefone deve ter entre 8 e 10 dígitos (sem DDD).")]
        public string Telefone { get; set; }

        public int? TipoUsuarioId { get; set; }
        public bool Ativo { get; set; }
    }
}