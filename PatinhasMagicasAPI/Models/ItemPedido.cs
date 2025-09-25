using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }
        public int Quantidade { get; set; }

        public int ProdutoId { get; set; }
        //public Produto Produto { get; set; }

        public int PedidoId { get; set; }
        //public Pedido Pedido { get; set; }

    }
}
