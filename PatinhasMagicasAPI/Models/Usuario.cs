using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido. Use 999.999.999-99.")]
        public string CPF { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [Display(Name = "E-mail")]
        [StringLength(50, ErrorMessage = "O e-mail deve ter no máximo 50 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O DDD é obrigatório.")]
        [Range(11, 99, ErrorMessage = "O DDD deve ser um número de 2 dígitos.")]
        public int Ddd { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "O telefone deve ter entre 8 e 10 dígitos.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Tipo de Usuário")]
        public int TipoUsuarioId { get; set; }
        public virtual TipoUsuario? TipoUsuario { get; set; }

    }
}
