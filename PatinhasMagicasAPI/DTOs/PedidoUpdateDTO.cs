using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PedidoUpdateDTO
    {
        [Required(ErrorMessage = "O campo StatusPedidoId é obrigatório.")]
        public int StatusPedidoId { get; set; }
        //public int TipoPagamentoId { get; set; }

    }
}
