using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.Models
{
    public class AgendamentoServico
    {
        [Key]
        public int Id { get; set; }
  
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O AgendamentoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "AgendamentoId deve ser um valor válido.")]
        public int AgendamentoId { get; set; }

        public Agendamento Agendamento { get; set; }

        [Required(ErrorMessage = "O ServicoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "ServicoId deve ser um valor válido.")]
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
    }
}
