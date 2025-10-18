using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        //public string Especie { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public string UrlImagem { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string DescricaoDetalhada { get; set; }
        public DateTime Validade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Tipo de Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
