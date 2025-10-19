using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.Models
{
    public class ServicoTamanho
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        [ForeignKey("Servico")]
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        [Required]
        [ForeignKey("TamanhoAnimal")]
        public int TamanhoAnimalId { get; set; }
        public TamanhoAnimal TamanhoAnimal { get; set; }
    }
}
