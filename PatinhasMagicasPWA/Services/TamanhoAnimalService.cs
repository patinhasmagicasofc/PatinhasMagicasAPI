using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class TamanhoAnimalService
    {
        private readonly HttpClient _http;

        public TamanhoAnimalService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TamanhoAnimalDTO>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<List<TamanhoAnimalDTO>>("api/tamanhoanimal");

            return response;
        }
    }
}
