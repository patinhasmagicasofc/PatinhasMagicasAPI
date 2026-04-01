using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class TipoPagamentoService
    {
        private readonly HttpClient _http;

        public TipoPagamentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TipoPagamentoDTO>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<List<TipoPagamentoDTO>>("api/tipopagamento");
            return response ?? new List<TipoPagamentoDTO>();
        }
    }
}
