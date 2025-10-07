namespace PatinhasMagicasAPI.DTOs
{
    public class ProdutoOutputDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Foto { get; set; }
        public string Codigo { get; set; }
        public DateTime Validade { get; set; }
        public int CategoriaId { get; set; }
    }
}
