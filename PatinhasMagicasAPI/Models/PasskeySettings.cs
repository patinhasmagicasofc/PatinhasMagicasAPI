namespace PatinhasMagicasAPI.Models
{
    public class PasskeySettings
    {
        public string RpId { get; set; } = "localhost";
        public string RpName { get; set; } = "Patinhas Magicas";
        public List<string> Origins { get; set; } = new();
    }
}
