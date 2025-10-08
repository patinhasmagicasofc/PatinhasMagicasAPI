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

        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        // GET: api/Endereco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
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

        // POST: api/Endereco
        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco([FromBody] EnderecoInputDTO enderecoDto)
        {
            // Mapear DTO para a Entidade Endereco
            var endereco = new Endereco
            {
                Logradouro = enderecoDto.Logradouro,
                Numero = enderecoDto.Numero,
                Bairro = enderecoDto.Bairro, // Assumindo que este campo está no seu DTO real
                Cidade = enderecoDto.Cidade, // Assumindo que este campo está no seu DTO real
                Estado = enderecoDto.Estado, // Assumindo que este campo está no seu DTO real
                CEP = enderecoDto.CEP,
                Complemento = enderecoDto.Complemento,
                UsuarioId = enderecoDto.UsuarioId
            };

            await _enderecoRepository.AddAsync(endereco);


            if (await _enderecoRepository.SaveChangesAsync())
            {

                return CreatedAtAction("GetEndereco", new { id = endereco.IdEndereco }, endereco);
            }

            return BadRequest("Falha ao criar o endereço.");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, [FromBody] EnderecoInputDTO enderecoDto)
        {
  
            if (id != enderecoDto.IdEndereco)
            {
                return BadRequest("O ID na URL não corresponde ao ID do endereço no corpo da requisição.");
            }


            var existingEndereco = await _enderecoRepository.GetByIdAsync(id);
            if (existingEndereco == null)
            {
                return NotFound();
            }


            existingEndereco.Logradouro = enderecoDto.Logradouro;
            existingEndereco.Numero = enderecoDto.Numero;
            existingEndereco.Bairro = enderecoDto.Bairro;
            existingEndereco.Cidade = enderecoDto.Cidade;
            existingEndereco.Estado = enderecoDto.Estado;
            existingEndereco.CEP = enderecoDto.CEP;
            existingEndereco.Complemento = enderecoDto.Complemento;

            existingEndereco.UsuarioId = enderecoDto.UsuarioId;

            // await _enderecoRepository.UpdateAsync(endereco); 

            if (await _enderecoRepository.SaveChangesAsync())
            {

                return NoContent();
            }


            return BadRequest("Falha ao salvar as alterações do endereço.");
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

            await _enderecoRepository.DeleteAsync(endereco);

            if (await _enderecoRepository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Falha ao deletar o endereço.");
        }
    }
}