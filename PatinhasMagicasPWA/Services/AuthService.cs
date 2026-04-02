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

                if (!response.IsSuccessStatusCode)
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = "'a'"
                    };
                }

                var content = await response.Content.ReadAsStringAsync();
                var token = ExtractToken(content);

                if (string.IsNullOrWhiteSpace(token))
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = "A API respondeu ao login, mas nao retornou um token valido."
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
