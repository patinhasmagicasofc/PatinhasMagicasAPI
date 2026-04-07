using System.Text.Json;

namespace PatinhasMagicasPWA.DTOs
{
    public class PasskeyCompleteRequestDTO
    {
        public string FlowId { get; set; } = string.Empty;
        public JsonElement Credential { get; set; }
    }
}
