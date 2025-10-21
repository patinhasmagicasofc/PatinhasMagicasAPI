using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
