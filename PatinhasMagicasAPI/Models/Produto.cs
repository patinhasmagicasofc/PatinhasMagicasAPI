using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Foto { get; set; }
        public string Codigo { get; set; }
        public DateTime Validade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Tipo de Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
