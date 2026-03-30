using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasPWA.DTOs
{
    public class CadastroUsuarioDTO
    {
        [Required(ErrorMessage = "O nome e obrigatorio.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no maximo 150 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O CPF e obrigatorio.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF invalido. Use 999.999.999-99.")]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "O e-mail e obrigatorio.")]
        [EmailAddress(ErrorMessage = "O e-mail informado nao e valido.")]
        [StringLength(50, ErrorMessage = "O e-mail deve ter no maximo 50 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O DDD e obrigatorio.")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "O DDD deve ter 2 digitos.")]
        public int? Ddd { get; set; }

        [Required(ErrorMessage = "O telefone e obrigatorio.")]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "O telefone deve ter entre 8 e 10 digitos.")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "A senha e obrigatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha.")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas nao coincidem.")]
        public string? ConfirmarSenha { get; set; }
    }
}
