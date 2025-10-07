namespace PatinhasMagicasAPI.DTOs
{
    public class PedidoFiltroDTO
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Nome { get; set; }
        public string? Status { get; set; }

        // Opcional: paginação
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
