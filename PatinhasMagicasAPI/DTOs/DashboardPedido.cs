namespace PatinhasMagicasAPI.DTOs
{
    public class DashboardPedido
    {
        public List<PedidoOutputDTO> PedidoOutputDTO { get; set; }
        public int QTotalVendas { get; set; }
        public int QPedidosCancelado { get; set; }
        public int QPedidosPendente { get; set; }
        public decimal ValorTotalVendas { get; set; }

    }
}
