using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepository _enderecoRepository;

        // Constructor for dependency injection
        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        // GET: api/Endereco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoInputDTO>>> GetEnderecos()
        {
            var enderecos = await _enderecoRepository.GetAsync();
            return Ok(enderecos);
        }

        // GET: api/Endereco/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);

            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(endereco);
        }

        // Post
        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco([FromBody] EnderecoInputDTO enderecoDto) 
        {
            // 2. Mapear DTO para a Entidade Endereco antes de salvar
            var endereco = new Endereco
            {
                Logradouro = enderecoDto.Logradouro,
                Numero = enderecoDto.Numero,
                Bairro = enderecoDto.Bairro,
                Cidade = enderecoDto.Cidade,
                Estado = enderecoDto.Estado,
                CEP = enderecoDto.CEP,
                Complemento = enderecoDto.Complemento,
                UsuarioId = enderecoDto.UsuarioId // Apenas o ID
            };

            _enderecoRepository.AddAsync(endereco);
            await _enderecoRepository.SaveChangesAsync();

            return CreatedAtAction("GetEndereco", new { id = endereco.IdEndereco }, endereco);
        }

        // PUT: api/Endereco/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (id != endereco.IdEndereco)
            {
                return BadRequest();
            }

            _enderecoRepository.UpdateAsync(endereco);
            await _enderecoRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Endereco/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            _enderecoRepository.DeleteAsync(endereco);
            await _enderecoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}