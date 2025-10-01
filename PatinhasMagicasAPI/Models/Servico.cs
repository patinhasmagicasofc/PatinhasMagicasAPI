namespace PatinhasMagicasAPI.Models
{
    public class Servico
    {
        public int Id{ get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }

        // FK
        public int TipoServicoId { get; set; }
        public TipoServico TipoServico { get; set; }
    }
}
