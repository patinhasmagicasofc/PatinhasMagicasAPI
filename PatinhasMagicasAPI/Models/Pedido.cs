using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }

        public int ClienteId { get; set; }
        //public Cliente Cliente { get; set; }

        public int UsuarioId { get; set; }
        //public Usuario Usuario { get; set; }

        public int StatusPedidoId { get; set; }
        //public StatusPedido StatusPedido { get; set; }
    }
}
