using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O PedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "PedidoId deve ser um valor válido.")]
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        [Required(ErrorMessage = "O StatusAgendamentoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusAgendamentoId deve ser um valor válido.")]
        public int IdStatusAgendamento { get; set; }
        public StatusAgendamento? StatusAgendamento { get; set; }
    }
}
