using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Pagamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataPagamento { get; set; } = DateTime.Now;
        public decimal Valor { get; set; }

        public string  Observacao { get; set; }

        public int StatusPagamentoId { get; set; }
        public StatusPagamento? StatusPagamento { get; set; }


        public int TipoPagamentoId { get; set; }
        public TipoPagamento? TipoPagamento { get; set; }


        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
