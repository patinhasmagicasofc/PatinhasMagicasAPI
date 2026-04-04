using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class ProdutoService
    {
        private readonly HttpClient _http;

        public ProdutoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProdutoDTO>> GetAllProdutosAsync()
        {
            var response = await _http.GetFromJsonAsync<List<ProdutoDTO>>("api/produto");

            return response;
        }
    }
}
