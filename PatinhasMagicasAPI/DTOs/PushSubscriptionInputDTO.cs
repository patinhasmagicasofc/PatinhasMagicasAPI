using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PushSubscriptionInputDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string Endpoint { get; set; } = string.Empty;

        [Required]
        public string P256DH { get; set; } = string.Empty;

        [Required]
        public string Auth { get; set; } = string.Empty;
    }
}
