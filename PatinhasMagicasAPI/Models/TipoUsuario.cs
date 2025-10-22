using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do tipo de usuário é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome do tipo de usuário deve ter no máximo 150 caracteres.")]
        [Display(Name = "Tipo de Usuário")]
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
