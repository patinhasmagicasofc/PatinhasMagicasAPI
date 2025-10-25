namespace PatinhasMagicasAPI.DTOs
{
    public class ProdutoOutputDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public string? Marca { get; set; }
        public string? UrlImagem { get; set; }
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public string? DescricaoDetalhada { get; set; }
        public DateTime? Validade { get; set; }
        public int? CategoriaId { get; set; }
        public string? CategoriaNome { get; set; }
        public string? EspecieId { get; set; }
        public string? EspecieNome { get; set; }
    }
}
