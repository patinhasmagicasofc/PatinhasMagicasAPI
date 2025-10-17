using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        // Propriedade de navegação
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
