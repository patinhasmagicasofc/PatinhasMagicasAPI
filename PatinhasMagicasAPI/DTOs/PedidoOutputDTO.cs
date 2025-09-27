using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PedidoOutputDTO
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int StatusPedidoId { get; set; }
    }
}
