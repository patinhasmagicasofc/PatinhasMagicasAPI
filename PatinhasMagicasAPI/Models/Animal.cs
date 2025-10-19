using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.Models
{
    public class Animal
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A espécie é obrigatória.")]
        [StringLength(50, ErrorMessage = "A espécie não pode exceder 50 caracteres.")]
        public string Especie { get; set; }

        [StringLength(100, ErrorMessage = "A raça não pode exceder 100 caracteres.")]
        public string Raca { get; set; }

        [Range(0, 100, ErrorMessage = "A idade deve ser entre 0 e 100 anos.")]
        public int? Idade { get; set; }

        [Required(ErrorMessage = "O tamanho do animal é obrigatório.")]
        [ForeignKey("TamanhoAnimal")]
        public int TamanhoAnimalId { get; set; }

        public TamanhoAnimal TamanhoAnimal { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
