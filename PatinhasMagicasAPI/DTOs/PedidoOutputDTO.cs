namespace PatinhasMagicasAPI.DTOs
{
    public class PedidoOutputDTO
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public int StatusPedidoId { get; set; }
        public string StatusPedido { get; set; }
        public string StatusPagamento { get; set; }
        public string FormaPagamento { get; set; }
        public decimal ValorPedido { get; set; }
        public UsuarioOutputDTO? UsuarioOutputDTO { get; set; }
        public List<ItemPedidoOutputDTO>? ItemPedidoOutputDTOs { get; set; }
    }
}
