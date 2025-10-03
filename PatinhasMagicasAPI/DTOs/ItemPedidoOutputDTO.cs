namespace PatinhasMagicasAPI.DTOs
{
    public class ItemPedidoOutputDTO
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int ProdutoId { get; set; }
        public int PedidoId { get; set; }

        public ProdutoOutputDTO? ProdutoOutputDTO { get; set; }
    }
}
