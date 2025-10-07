using System.Text.Json;

public class CepService
{
    private readonly HttpClient _httpClient;

    public CepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CepOutputDTO?> BuscarCepAsync(string cep)
    {
        var url = $"https://viacep.com.br/ws/{cep}/json/";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var cepDto = JsonSerializer.Deserialize<CepOutputDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return cepDto;
    }
}
