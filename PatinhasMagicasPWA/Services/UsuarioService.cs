using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

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

        public async Task<bool> Login(UsuarioDTO usuario)
        {
            var response = await _http.PostAsJsonAsync("api/usuario", usuario);

        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            await _tokenStorageService.SetToken(token);
        }

        return true;
    }
}
}
