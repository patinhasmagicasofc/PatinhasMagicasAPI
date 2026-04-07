using System.Text.Json;

namespace PatinhasMagicasAPI.DTOs
{
    public class PasskeyCompleteRequestDTO
    {
        public string FlowId { get; set; } = string.Empty;
        public JsonElement Credential { get; set; }
    }
}
