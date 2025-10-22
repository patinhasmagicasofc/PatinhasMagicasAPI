using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Especie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da espécie é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da espécie não pode exceder 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Animal> Animais { get; set; } = new List<Animal>();
    }
}
