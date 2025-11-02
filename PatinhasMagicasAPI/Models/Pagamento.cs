using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.Models
{
    public class Pagamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data de pagamento é obrigatória.")]
        public DateTime DataPagamento { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [StringLength(255, ErrorMessage = "A observação deve ter no máximo 255 caracteres.")]
        public string? Observacao { get; set; }

        //[Required(ErrorMessage = "O status do pagamento é obrigatório.")]
        //[Range(1, int.MaxValue, ErrorMessage = "StatusPagamentoId deve ser um valor válido.")]
        public int? StatusPagamentoId { get; set; }
        public StatusPagamento? StatusPagamento { get; set; }

        [Required(ErrorMessage = "O tipo de pagamento é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "TipoPagamentoId deve ser um valor válido.")]
        public int TipoPagamentoId { get; set; }
        public TipoPagamento? TipoPagamento { get; set; }

        [Required(ErrorMessage = "O pedido é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "PedidoId deve ser um valor válido.")]
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; } = null!;
    }
}
