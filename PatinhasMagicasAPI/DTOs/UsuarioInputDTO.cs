using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class UsuarioInputDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido. Use 999.999.999-99.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [Display(Name = "E-mail")]
        [StringLength(50, ErrorMessage = "O e-mail deve ter no máximo 50 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]

        public string Senha { get; set; }

        [Required(ErrorMessage = "O DDD é obrigatório.")]
        [Range(11, 99, ErrorMessage = "O DDD deve ser um número de 2 dígitos (entre 11 e 99).")]
        public int Ddd { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "O telefone deve ter entre 8 e 10 dígitos (sem DDD).")]
        public string Telefone { get; set; }

        [Display(Name = "Tipo de Usuário")]
        [Range(1, int.MaxValue, ErrorMessage = "O Tipo de Usuário é inválido.")]
        public int? TipoUsuarioId { get; set; }
    }
}