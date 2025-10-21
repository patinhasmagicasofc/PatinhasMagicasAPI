using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class TipoPagamentoInputDTO
    {
        [Required(ErrorMessage = "O método de pagamento é obrigatório")]
        [StringLength(50, ErrorMessage = "O método deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
    }
}
