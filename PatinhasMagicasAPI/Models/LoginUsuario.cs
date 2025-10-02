using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class LoginUsuario
    {
        [Required(ErrorMessage = "Objeto E-mail é obrigatório!")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
