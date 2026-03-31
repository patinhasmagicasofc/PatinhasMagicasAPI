using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class PushSubscriptionRemoveDTO
    {
        [Required]
        public string Endpoint { get; set; } = string.Empty;
    }
}
