using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class StatusPagamento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O status do pagamento é obrigatório")]
        [StringLength(30, ErrorMessage = "O status deve ter no máximo 30 caracteres")]
        public string Nome { get; set; }    
    }
}
