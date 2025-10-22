using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class TipoServico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do tipo de serviço é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do tipo de serviço deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;


        public virtual ICollection<Servico> Servicos { get; set; } = new List<Servico>();
    }
}

