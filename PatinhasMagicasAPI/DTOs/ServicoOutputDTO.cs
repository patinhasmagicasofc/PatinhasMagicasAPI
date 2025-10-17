namespace PatinhasMagicasAPI.DTOs
{
    public class ServicoOutputDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? DescricaoDetalhada { get; set; }
        public int? TempoEstimadoMinutos { get; set; }
        public bool? Ativo { get; set; }
        public int TipoServicoId { get; set; }
    }
}
