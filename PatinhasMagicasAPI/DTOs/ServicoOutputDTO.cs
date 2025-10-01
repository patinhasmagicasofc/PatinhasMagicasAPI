using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.DTOs
{
    public class ServicoOutputDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int TipoServicoId { get; set; }
    }
}
