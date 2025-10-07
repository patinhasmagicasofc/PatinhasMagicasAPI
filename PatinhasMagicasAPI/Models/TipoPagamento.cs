using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TipoPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório")]
        [StringLength(50, ErrorMessage = "O método deve ter no máximo 100 caracteres")]
        public string Metodo { get; set; }

        //public MetodoPagamento Metodo { get; set; }

        //public enum MetodoPagamento
        //{
        //    Dinheiro,
        //    Pix,
        //    CartaoCredito,
        //    CartaoDebito
        //}
    }
}
