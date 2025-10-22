using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;


        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
