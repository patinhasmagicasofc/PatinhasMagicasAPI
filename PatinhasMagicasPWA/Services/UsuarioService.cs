using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace PatinhasMagicasPWA.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;
        private readonly TokenStorageService _tokenStorageService;

        public UsuarioService(HttpClient http, TokenStorageService tokenStorageService)
        {
            _http = http;
            _tokenStorageService = tokenStorageService;
        }

        public async Task<bool> Cadastrar(UsuarioDTO usuario)
        {
            var response = await _http.PostAsJsonAsync("api/usuario", usuario);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var token = ExtractToken(content);

            if (!string.IsNullOrWhiteSpace(token))
            {
                await _tokenStorageService.SetToken(token);
            }

            return true;
        }

        private static string? ExtractToken(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            try
            {
                var loginResponse = JsonSerializer.Deserialize<LoginResponseDTO>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (!string.IsNullOrWhiteSpace(loginResponse?.Data?.Token))
                {
                    return loginResponse.Data.Token.Trim();
                }
            }
            catch (JsonException)
            {
            }

            try
            {
                var rawToken = JsonSerializer.Deserialize<string>(content);

                if (!string.IsNullOrWhiteSpace(rawToken))
                {
                    return rawToken.Trim();
                }
            }
            catch (JsonException)
            {
            }

            return content.Trim().Trim('"');
        }
    }
}
