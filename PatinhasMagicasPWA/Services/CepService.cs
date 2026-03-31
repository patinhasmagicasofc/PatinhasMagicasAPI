using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class CepService
    {
        private readonly HttpClient _http;

        public CepService(HttpClient http)
        {
            _http = http;
        }

        public async Task<CepOutputDTO?> BuscarCepAsync(string cep)
        {
            var cepLimpo = new string((cep ?? string.Empty).Where(char.IsDigit).ToArray());

            if (cepLimpo.Length != 8)
            {
                return null;
            }

            var response = await _http.GetAsync($"api/cep/buscar/{cepLimpo}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CepOutputDTO>();
        }
    }
}
