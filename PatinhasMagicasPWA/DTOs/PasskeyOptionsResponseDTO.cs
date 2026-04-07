using System.Text.Json;

namespace PatinhasMagicasPWA.DTOs
{
    public class PasskeyOptionsResponseDTO
    {
        public string FlowId { get; set; } = string.Empty;
        public JsonElement PublicKey { get; set; }
    }
}
