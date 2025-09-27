using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser no mínimo 1.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O ProdutoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProdutoId deve ser um valor válido.")]
        public int ProdutoId { get; set; }
        // public Produto Produto { get; set; }

        [Required(ErrorMessage = "O PedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "PedidoId deve ser um valor válido.")]
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
