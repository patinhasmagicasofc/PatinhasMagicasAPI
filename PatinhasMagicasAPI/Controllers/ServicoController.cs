using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoController(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoOutputDTO>>> GetServicos()
        {
            var servicos = await _servicoRepository.GetAllAsync();

            if (!servicos.Any())
                return NotFound();

            var servicoOutputDTOs = servicos.Select(p => new ServicoOutputDTO
            {
                Id = p.Id,
                Nome = p.Nome,
            }).ToList();

            return Ok(servicoOutputDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoOutputDTO>> GetById(int id)
        {
            var servico = await _servicoRepository.GetByIdAsync(id);

            if (servico == null)
                return NotFound();

            var servicoDTO = new ServicoOutputDTO
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                TipoServicoId = servico.TipoServicoId,
            };

            return Ok(servicoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ServicoOutputDTO>> PostServico(ServicoInputDTO servicoInputDTO)
        {
            var servico = new Servico
            {
                Nome = servicoInputDTO.Nome,
                Descricao = servicoInputDTO.Descricao,
                DescricaoDetalhada = servicoInputDTO.DescricaoDetalhada,
                TempoEstimadoMinutos = servicoInputDTO.TempoEstimadoMinutos,
                Ativo = servicoInputDTO.Ativo,
                TipoServicoId = servicoInputDTO.TipoServicoId
            };

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _servicoRepository.AddAsync(servico);

            var servicoOutputDTO = new ServicoOutputDTO
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                DescricaoDetalhada = servico.DescricaoDetalhada,
                TempoEstimadoMinutos = servico.TempoEstimadoMinutos,
                Ativo = servico.Ativo,
                TipoServicoId = servico.TipoServicoId
            };

            return Ok(new { success = true, message = "Serviço cadastrado com sucesso!", servicoOutputDTO });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServico(int id, ServicoInputDTO servicoInputDTO)
        {
            var servico = await _servicoRepository.GetByIdAsync(id);

            if (servico == null)
                return NotFound();

            servico = new Servico
            {
                Id = id,
                Nome = servicoInputDTO.Nome,
                Descricao = servicoInputDTO.Descricao,
                TipoServicoId = servicoInputDTO.TipoServicoId
            };

            await _servicoRepository.UpdateAsync(servico);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServico(int id)
        {
            var servico = await _servicoRepository.GetByIdAsync(id);
            if (servico == null)
                return NotFound();

            await _servicoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}