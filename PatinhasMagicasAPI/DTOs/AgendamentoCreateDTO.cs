namespace PatinhasMagicasAPI.DTOs
{
    public class AgendamentoCreateDTO
    {
        public int? UsuarioId { get; set; }
        public int AnimalId { get; set; }
        public int ServicoId { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public int? TipoPagamentoId{ get; set; }
    }
}
