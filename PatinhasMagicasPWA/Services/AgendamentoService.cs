using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class AgendamentoService
    {
        private readonly HttpClient _http;

        public AgendamentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AgendamentoDTO>> GetAll()
        {
            var response = await _http.GetFromJsonAsync<List<AgendamentoDTO>>("api/agendamento/meus");

            return response;
        }
    }
}
