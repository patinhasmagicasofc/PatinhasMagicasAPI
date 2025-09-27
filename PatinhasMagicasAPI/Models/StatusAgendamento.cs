using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class StatusAgendamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do status é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do status deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public ICollection<Agendamento>? Agendamentos { get; set; }
    }
}
