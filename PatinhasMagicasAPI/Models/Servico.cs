using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode ter mais de 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "A descrição não pode ter mais de 255 caracteres.")]
        public string? Descricao { get; set; }

        public string? DescricaoDetalhada { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O tempo estimado deve ser positivo.")]
        public int? TempoEstimadoMinutos { get; set; }


        [Required(ErrorMessage = "O TipoServicoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "TipoServicoId deve ser um valor válido.")]
        public int TipoServicoId { get; set; }

        public virtual TipoServico TipoServico { get; set; } = null!;

        public virtual ICollection<AgendamentoServico> AgendamentoServicos { get; set; } = new List<AgendamentoServico>();

    }
}
