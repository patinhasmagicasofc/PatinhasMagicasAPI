using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "A raça não pode exceder 100 caracteres.")]
        public string? Raca { get; set; }

        [Range(0, 100, ErrorMessage = "A idade deve ser entre 0 e 100 anos.")]
        public int? Idade { get; set; }

        [Required(ErrorMessage = "A espécie é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "EspecieId deve ser um valor válido.")]
        public int EspecieId { get; set; }
        public virtual Especie Especie { get; set; } = null!;

        [Required(ErrorMessage = "O tamanho do animal é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "TamanhoAnimalId deve ser um valor válido.")]
        public int TamanhoAnimalId { get; set; }
        public virtual TamanhoAnimal TamanhoAnimal { get; set; } = null!;

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser um valor válido.")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;


        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}
