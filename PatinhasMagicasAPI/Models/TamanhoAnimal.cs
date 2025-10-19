using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TamanhoAnimal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do tamanho é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do tamanho não pode exceder 50 caracteres.")]
        public string Nome { get; set; }

        public ICollection<Animal> Animais { get; set; }
    }
}
