using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class StatusPedido
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
