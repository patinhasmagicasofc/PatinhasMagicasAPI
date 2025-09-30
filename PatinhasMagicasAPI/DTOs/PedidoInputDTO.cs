using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PedidoInputDTO
    {
        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime DataPedido { get; set; }

        [Required(ErrorMessage = "O ClienteId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "ClienteId deve ser um valor válido.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O UsuarioId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser um valor válido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O StatusPedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusPedidoId deve ser um valor válido.")]
        public int StatusPedidoId { get; set; }
    }
}
