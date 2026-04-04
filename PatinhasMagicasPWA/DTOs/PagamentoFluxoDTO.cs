using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasPWA.DTOs
{
    public class PagamentoFluxoDTO
    {
        [Required(ErrorMessage = "Selecione a forma de pagamento.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma forma de pagamento válida.")]
        public int? TipoPagamentoId { get; set; }
    }
}
