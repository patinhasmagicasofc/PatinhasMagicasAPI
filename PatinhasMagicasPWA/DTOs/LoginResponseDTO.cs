namespace PatinhasMagicasPWA.DTOs
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public LoginResponseDataDTO? Data { get; set; }
    }

    public class LoginResponseDataDTO
    {
        public string? Token { get; set; }
    }
}
