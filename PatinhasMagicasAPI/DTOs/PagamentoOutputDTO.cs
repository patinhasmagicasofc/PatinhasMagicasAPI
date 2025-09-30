namespace PatinhasMagicasAPI.DTOs
{
    public class PagamentoOutputDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
        public int TipoPagamentoId { get; set; }
        public int PedidoId { get; set; }
    }
}
