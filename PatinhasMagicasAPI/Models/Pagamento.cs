namespace PatinhasMagicasAPI.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }


        public int TipoPagamentoId { get; set; }
        public TipoPagamento? TipoPagamento { get; set; }


        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
