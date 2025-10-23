using PatinhasMagicasAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class EspecieInputDTO
    {

        [Required(ErrorMessage = "O nome da espécie é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da espécie não pode exceder 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;
    }
}
