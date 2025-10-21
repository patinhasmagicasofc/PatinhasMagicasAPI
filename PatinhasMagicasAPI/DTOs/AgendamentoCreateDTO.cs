namespace PatinhasMagicasAPI.DTOs
{
    public class AgendamentoCreateDTO
    {
        public int? IdUsuario { get; set; }
        public int IdAnimal { get; set; }
        public List<int>? IdServicos { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public int? IdTipoPagamento{ get; set; }
    }
}
