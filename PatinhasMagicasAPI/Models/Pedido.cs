using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime DataPedido { get; set; }

        [Required(ErrorMessage = "O UsuarioId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser um valor válido.")]
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "O StatusPedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusPedidoId deve ser um valor válido.")]
        public int StatusPedidoId { get; set; }
        public virtual StatusPedido StatusPedido { get; set; } = null!;

        public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }


}
