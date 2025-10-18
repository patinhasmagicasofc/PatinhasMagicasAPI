namespace PatinhasMagicasAPI.DTOs
{
    public class ProdutoInputDTO
    {
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        //public string? Especie { get; set; }
        public string? Marca { get; set; }
        public string? UrlImagem { get; set; }
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public string? DescricaoDetalhada { get; set; }
        public DateTime? Validade { get; set; }
        public int? CategoriaId { get; set; }

    }
}
