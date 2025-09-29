using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PagamentoInputDTO
    {
        [Required(ErrorMessage = "O status do pagamento é obrigatório")]
        [StringLength(30, ErrorMessage = "O status deve ter no máximo 30 caracteres")]
        public string Status { get; set; }
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O tipo de pagamento é obrigatório")]
        public int TipoPagamentoId { get; set; }

        [Required(ErrorMessage = "O pedido é obrigatório")]
        public int PedidoId { get; set; }
    }
}
