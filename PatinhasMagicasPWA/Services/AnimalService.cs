using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class AnimalService
    {
        private readonly HttpClient _http;

        public AnimalService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AnimalDTO>> GetAll()
        {
            var response = await _http.GetFromJsonAsync<List<AnimalDTO>>("api/animal/meus");

            return response;
        }
    }
}
