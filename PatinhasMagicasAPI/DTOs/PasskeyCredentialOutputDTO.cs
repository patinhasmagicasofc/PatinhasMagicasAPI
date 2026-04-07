namespace PatinhasMagicasAPI.DTOs
{
    public class PasskeyCredentialOutputDTO
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
    }
}
