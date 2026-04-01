using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

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

        public async Task<AgendamentoDTO?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<AgendamentoDTO>($"api/agendamento/{id}");
        }

        public async Task<AgendamentoDTO?> CreateAsync(AgendamentoCreateDTO createDTO)
        {
            var response = await _http.PostAsJsonAsync("api/agendamento", createDTO);
            if (!response.IsSuccessStatusCode) return null;

            // API returns { success = true, message = "...", agendamento = agendamento }
            var content = await response.Content.ReadFromJsonAsync<JsonElement>();
            if (content.ValueKind == JsonValueKind.Object && content.TryGetProperty("agendamento", out var agendamentoProp))
            {
                try
                {
                    var agendamento = JsonSerializer.Deserialize<AgendamentoDTO>(agendamentoProp.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return agendamento;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}
