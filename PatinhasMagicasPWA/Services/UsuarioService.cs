using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Login(UsuarioDTO usuario)
        {
            var response = await _http.PostAsJsonAsync("api/usuario", usuario);

            if (!response.IsSuccessStatusCode)
                return false;

            var token = await response.Content.ReadAsStringAsync();

            return true;
        }
    }
}
