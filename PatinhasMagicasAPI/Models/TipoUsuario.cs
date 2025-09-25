using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TipoUsuario
    {
        [Key]
        public int IdTipoUsuario { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Tipo de Usuário")]
        [StringLength(150)]
        public string DescricaoTipoUsuario { get; set; }

        // Propriedade de navegação
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
