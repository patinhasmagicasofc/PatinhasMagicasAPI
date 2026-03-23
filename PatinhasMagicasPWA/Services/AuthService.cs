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

        public async Task<bool> Login(LoginDTO login)
        {
            var response = await _http.PostAsJsonAsync("api/login", login);

        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadAsStringAsync();

        await _tokenStorageService.SetToken(token);
        return true;
    }

    public async Task Logout()
    {
        await _tokenStorageService.RemoveToken();
    }
}
}
