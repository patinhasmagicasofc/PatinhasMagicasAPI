using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O PedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "PedidoId deve ser um valor válido.")]
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        [Required(ErrorMessage = "O AnimalId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "AnimalId deve ser um valor válido.")]

        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        [Required(ErrorMessage = "O StatusAgendamentoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusAgendamentoId deve ser um valor válido.")]
        public int StatusAgendamentoId { get; set; }
        public StatusAgendamento? StatusAgendamento { get; set; }

        public ICollection<AgendamentoServico> AgendamentoServicos { get; set; } = new List<AgendamentoServico>();

    }
}
