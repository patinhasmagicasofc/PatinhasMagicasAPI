using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CepController : ControllerBase
{
    private readonly CepService _cepService;

    public CepController(CepService cepService)
    {
        _cepService = cepService;
    }

    [HttpGet("buscar/{cep}")]
    public async Task<ActionResult<CepOutputDTO>> BuscarCep(string cep)
    {
        string cepLimpo = cep.Replace("-", "").Replace(".", "");

        if (string.IsNullOrEmpty(cepLimpo) || cepLimpo.Length != 8)
        {
            return BadRequest(new { Message = "CEP inválido. Deve conter 8 dígitos." });
        }

        var cepDto = await _cepService.BuscarCepAsync(cepLimpo);

        // Verificação adicional para o caso de o ViaCEP retornar o objeto com a flag 'erro: true'
        if (cepDto == null || (cepDto.GetType().GetProperty("Erro") != null && (bool)cepDto.GetType().GetProperty("Erro").GetValue(cepDto)))
        {
            return NotFound(new { Message = "CEP não encontrado pelo serviço externo." });
        }

        return Ok(cepDto);
    }
}