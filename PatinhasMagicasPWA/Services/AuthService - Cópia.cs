using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Login(LoginDTO login)
        {
            var response = await _http.PostAsJsonAsync("api/login", login);

            if (!response.IsSuccessStatusCode)
                return false;

            var token = await response.Content.ReadAsStringAsync();

            // salvar token (localStorage depois)
            return true;
        }
    }
}
