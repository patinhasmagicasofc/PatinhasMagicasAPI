using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class AgendamentoInputDTO
    {
        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        public DateTime? DataAgendamento { get; set; }

        [Required(ErrorMessage = "O PedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "PedidoId deve ser um valor válido.")]
        public int? PedidoId { get; set; }

        [Required(ErrorMessage = "O StatusAgendamentoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusAgendamentoId deve ser um valor válido.")]
        public int? StatusAgendamentoId { get; set; }
    }
}
