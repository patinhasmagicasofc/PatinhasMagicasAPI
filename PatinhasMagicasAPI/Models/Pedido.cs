using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime DataPedido { get; set; }

        [Required(ErrorMessage = "O ClienteId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "ClienteId deve ser um valor válido.")]
        public int ClienteId { get; set; }
        public Usuario? Cliente { get; set; }

        [Required(ErrorMessage = "O UsuarioId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser um valor válido.")]
        public int UsuarioId { get; set; }
        // public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O StatusPedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusPedidoId deve ser um valor válido.")]
        public int StatusPedidoId { get; set; }
        // public StatusPedido StatusPedido { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

    }
}
