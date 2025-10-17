using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TipoPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O status do pagamento é obrigatório")]
        [StringLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres")]
        public string Nome { get; set; }
    }
}
