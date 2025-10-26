using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatinhasMagicasAPI.DTOs
{
    public class ProdutoInputDTO
    {

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A espécie é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "EspecieId deve ser um valor válido.")]
        public int EspecieId { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [StringLength(50, ErrorMessage = "A marca não pode exceder 50 caracteres.")]
        public string? Marca { get; set; }

        [StringLength(255, ErrorMessage = "A URL da imagem não pode exceder 255 caracteres.")]
        public string? UrlImagem { get; set; }

        [StringLength(50, ErrorMessage = "O código não pode exceder 50 caracteres.")]
        public string? Codigo { get; set; }

        [StringLength(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
        public string? Descricao { get; set; }

        public string? DescricaoDetalhada { get; set; }

        public DateTime? Validade { get; set; }

        [Required(ErrorMessage = "O tipo de categoria é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "CategoriaId deve ser um valor válido.")]
        public int CategoriaId { get; set; }

    }
}
