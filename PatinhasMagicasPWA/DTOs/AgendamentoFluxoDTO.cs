using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasPWA.DTOs
{
    public class AgendamentoFluxoDTO
    {
        [Required(ErrorMessage = "Selecione um animal.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um animal válido.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Selecione um serviço.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um serviço válido.")]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "Informe a data e hora do agendamento.")]
        public DateTime? DataAgendamento { get; set; }
    }
}
