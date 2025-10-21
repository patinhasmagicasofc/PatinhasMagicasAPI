using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.DTOs
{
    public class AgendamentoOutputDTO
    {
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PedidoId { get; set; }
        public int IdStatusAgendamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
        public List<AgendamentoServicoDTO> AgendamentoServicos { get; set; }
    }
}

