using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PatinhasMagicasPWA.Services
{
    public class ServicoService
    {
        private readonly HttpClient _http;

        public ServicoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ServicoOutputDTO>> GetAllServicosAsync()
        {
            var response = await _http.GetFromJsonAsync<List<ServicoOutputDTO>>("api/servico");

            return response;
        }

        public async Task<List<ServicoOutputDTO>> GetServicosPorAnimalAsync(int idAnimal)
        {
            var response = await _http.GetFromJsonAsync<List<ServicoOutputDTO>>($"api/servico/por-animal/{idAnimal}");

            return response;
        }

    }
}
