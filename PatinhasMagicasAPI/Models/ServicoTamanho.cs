using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.Models
{
    public class ServicoTamanho
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O ServicoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "ServicoId deve ser um valor válido.")]
        public int ServicoId { get; set; }
        public virtual Servico Servico { get; set; } = null!;

        [Required(ErrorMessage = "O TamanhoAnimalId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "TamanhoAnimalId deve ser um valor válido.")]
        public int TamanhoAnimalId { get; set; }
        public virtual TamanhoAnimal TamanhoAnimal { get; set; } = null!;

        public virtual ICollection<AgendamentoServico> AgendamentoServicos { get; set; } = new List<AgendamentoServico>();

    }
}
