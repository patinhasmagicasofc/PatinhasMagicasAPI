using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class PushSubscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(500)]
        public string Endpoint { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string P256DH { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Auth { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public DateTime? UltimoEnvioEm { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
