using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class TamanhoAnimalInputDTO
    {
        [Required(ErrorMessage = "O nome do tamanho é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do tamanho não pode exceder 50 caracteres.")]
        public string Nome { get; set; }
    }
}
