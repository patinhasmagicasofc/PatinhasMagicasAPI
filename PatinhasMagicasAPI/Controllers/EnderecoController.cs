using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Models.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoOutputDTO>>> GetEnderecos()
        {
            var enderecoOutputDTOs = await _enderecoService.GetAsync();

            return Ok(enderecoOutputDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoOutputDTO>> GetEndereco(int id)
        {
            var enderecoOutputDTO = await _enderecoService.GetByIdAsync(id);

            if (enderecoOutputDTO == null) return NotFound(new { message = "Endereço não encontrado." });

            return Ok(enderecoOutputDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco([FromBody] EnderecoInputDTO enderecoInputDTO) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _enderecoService.AddAsync(enderecoInputDTO);

            //return CreatedAtAction("GetEndereco", new { id = enderecoInputDTO.IdEndereco }, enderecoInputDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, EnderecoInputDTO enderecoInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _enderecoService.UpdateAsync(id, enderecoInputDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            await _enderecoService.DeleteAsync(id);

            return NoContent();
        }
    }
}