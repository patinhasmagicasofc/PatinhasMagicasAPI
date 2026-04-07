using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace PatinhasMagicasPWA.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly TokenStorageService _tokenStorageService;

        public AuthService(HttpClient http, TokenStorageService tokenStorageService)
        {
            _http = http;
            _tokenStorageService = tokenStorageService;
        }

        private async Task<LoginDTO> MapUsuarioToLogin(string email, string senha)
        {
            return new LoginDTO
            {
                Email = email,
                Senha = senha
            };
        }

        public async Task<LoginResultDTO> Login(string email, string senha)
        {
            var login = await MapUsuarioToLogin(email, senha);

            try
            {
                var response = await _http.PostAsJsonAsync("api/login", login);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = ExtractErrorMessage(content)
                    };
                }

                var loginResponse = JsonSerializer.Deserialize<LoginResponseDTO>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var token = loginResponse?.Data?.Token;

                if (string.IsNullOrWhiteSpace(token))
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = loginResponse?.Message ?? "A API respondeu ao login, mas nao retornou um token valido."
                    };
                }

                await _tokenStorageService.SetToken(token);

                return new LoginResultDTO
                {
                    Sucesso = true
                };
            }
            catch
            {
                return new LoginResultDTO
                {
                    Sucesso = false,
                    Mensagem = "Erro ao conectar com a API."
                };
            }
        }

        public async Task Logout()
        {
            await _tokenStorageService.RemoveToken();
        }

        public static string ExtractErrorMessage(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return "Não foi possível autenticar.";
            }

            try
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);

                if (jsonElement.ValueKind == JsonValueKind.Object)
                {
                    if (jsonElement.TryGetProperty("message", out var messageProperty) ||
                        jsonElement.TryGetProperty("Message", out messageProperty))
                    {
                        var message = messageProperty.GetString();
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            return message;
                        }
                    }
                }
            }
            catch (JsonException)
            {
            }

            return content.Trim().Trim('\"');
        }
    }
}
