using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

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

        public async Task<LoginResultDTO> Login(LoginDTO login)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/login", login);

                if (!response.IsSuccessStatusCode)
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = response.StatusCode == System.Net.HttpStatusCode.Unauthorized
                            ? "Email ou senha invalidos."
                            : "Nao foi possivel fazer login agora. Tente novamente."
                    };
                }

                var token = await response.Content.ReadAsStringAsync();

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
    }
}
