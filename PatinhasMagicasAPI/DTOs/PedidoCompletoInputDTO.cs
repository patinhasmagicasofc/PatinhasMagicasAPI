using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PedidoCompletoInputDTO
    {
        [Required(ErrorMessage = "O UsuarioId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser um valor válido.")]
        public int UsuarioId { get; set; }
        public int? StatusPedidoId { get; set; }
        public int TipoPagamentoId { get; set; }
        public List<ItemPedidoInputDTO> ItensPedido { get; set; }
    }
}
