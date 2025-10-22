using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class StatusPedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do status do pedido é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do status deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
