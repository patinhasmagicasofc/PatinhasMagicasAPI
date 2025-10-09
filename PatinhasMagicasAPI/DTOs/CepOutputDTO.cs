public class CepOutputDTO
{
    // A propriedade 'cep' no ViaCEP
    public string Cep { get; set; }

    // As propriedades de endereço (ViaCEP usa minúsculas, mas o JsonSerializerOptions resolve)
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }

    // O ViaCEP retorna este campo se o CEP não for encontrado
    public bool Erro { get; set; }
}