using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class StatusAgendamentoInputDTO
    {
        [Required(ErrorMessage = "O nome do status é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do status deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }
    }
}
