using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class EspecieService
    {
        private readonly HttpClient _http;

        public EspecieService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EspecieOutputDTO>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<List<EspecieOutputDTO>>("api/especie");

            return response;
        }
    }
}
