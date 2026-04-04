using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class AgendamentoCreateDTO
    {
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "O Animal é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um animal válido.")]
        public int? AnimalId { get; set; }

        [Required(ErrorMessage = "O serviço é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um serviço válido.")]
        public int? ServicoId { get; set; }

        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        public DateTime? DataAgendamento { get; set; }

        [Required(ErrorMessage = "A forma de pagamento é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma forma de pagamento válida.")]
        public int? TipoPagamentoId { get; set; }
    }
}
