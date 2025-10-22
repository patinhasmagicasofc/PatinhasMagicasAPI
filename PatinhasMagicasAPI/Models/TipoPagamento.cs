using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TipoPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O tipo de pagamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do tipo de pagamento deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}
