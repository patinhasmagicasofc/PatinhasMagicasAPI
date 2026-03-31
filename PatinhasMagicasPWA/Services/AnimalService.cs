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

        public async Task<List<AnimalDTO>> GetAllAnimalsAsync()
        {
            var response = await _http.GetFromJsonAsync<List<AnimalDTO>>("api/animal/meus");

            return response;
        }

        public async Task<bool> Cadastrar(AnimalDTO animal)
        {
            var response = await _http.PostAsJsonAsync("api/animal", animal);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();

            return true;
        }

        public async Task<bool> Editar(AnimalDTO animal)
        {
            var response = await _http.PutAsJsonAsync($"api/animal/{animal.Id}", animal);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            return true;

        }

        public async Task<bool> Delete(AnimalDTO animal)
        {
            var response = await _http.DeleteAsync($"api/animal/{animal.Id}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            return true;
        }

        public async Task<AnimalDTO> GetById(int id)
        {
            var response = await _http.GetFromJsonAsync<AnimalDTO>($"api/animal/{id}");
            return response;
        }
    }
}
