using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class Servico
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode ter mais de 50 caracteres.")]
        public string Nome { get; set; }

        [StringLength(255, ErrorMessage = "A descrição não pode ter mais de 255 caracteres.")]
        public string? Descricao { get; set; }

        public string? DescricaoDetalhada { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O tempo estimado deve ser positivo.")]
        public int? TempoEstimadoMinutos { get; set; }
        public bool? Ativo { get; set; } = true;

        public int TipoServicoId { get; set; }
        public TipoServico TipoServico { get; set; }
    }
}
