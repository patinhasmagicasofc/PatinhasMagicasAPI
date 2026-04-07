using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models
{
    public class PasskeyCredential
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(200)]
        public string CredentialId { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string UserHandle { get; set; } = string.Empty;

        [Required]
        public string PublicKey { get; set; } = string.Empty;

        public uint SignatureCounter { get; set; }

        [StringLength(120)]
        public string FriendlyName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CredType { get; set; }

        [StringLength(100)]
        public string? AaGuid { get; set; }

        [StringLength(200)]
        public string? Transports { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUsedAt { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
