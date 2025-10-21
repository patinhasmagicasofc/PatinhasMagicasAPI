using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PagamentoInputDTO
    {
        public DateTime? DataPagamento { get; set; }
        public decimal? Valor { get; set; }
        public string? Observacao { get; set; }
        public int? TipoPagamentoId { get; set; }
        public int? StatusPagamentoId { get; set; }

        [Required(ErrorMessage = "O pedido é obrigatório")]
        public int? PedidoId { get; set; }
    }
}
