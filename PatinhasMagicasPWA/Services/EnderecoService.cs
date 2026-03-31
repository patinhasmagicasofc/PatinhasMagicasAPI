using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;

namespace PatinhasMagicasPWA.Services
{
    public class EnderecoService
    {
        private readonly HttpClient _http;

        public EnderecoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> UpdateAsync(int id, EnderecoOutputDTO endereco)
        {
            var updatePayload = new
            {
                endereco.Logradouro,
                endereco.Numero,
                endereco.Bairro,
                endereco.Cidade,
                endereco.Estado,
                endereco.CEP,
                endereco.Complemento,
                endereco.UsuarioId
            };

            var response = await _http.PutAsJsonAsync($"api/endereco/{id}", updatePayload);
            return response.IsSuccessStatusCode;
        }
    }
}
