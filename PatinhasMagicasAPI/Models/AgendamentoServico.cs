using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class AgendamentoServico
    {
        [Key]
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public Agendamento Agendamento { get; set; }
        public int AgendamentoId { get; set; }
        public Servico Servico { get; set; }
        public int ServicoId { get; set; }
    }
}
