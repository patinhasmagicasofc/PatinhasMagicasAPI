using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Pagamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O status do pagamento é obrigatório")]
        [StringLength(30, ErrorMessage = "O status deve ter no máximo 30 caracteres")]
        public string Status { get; set; }

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;


        public int TipoPagamentoId { get; set; }
        public TipoPagamento? TipoPagamento { get; set; }


        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
