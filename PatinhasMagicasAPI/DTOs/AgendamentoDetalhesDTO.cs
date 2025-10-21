namespace PatinhasMagicasAPI.DTOs
{

    public class AgendamentoDetalhesDTO
    {
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataConfirmacao { get; set; }
        public string Status { get; set; }

        public AnimalOutputDTO Animal { get; set; }
        public List<ServicoOutputDTO> Servicos { get; set; }
        public string TipoPagamento { get; set; }
        public decimal ValorTotal { get; set; }

    }
}
