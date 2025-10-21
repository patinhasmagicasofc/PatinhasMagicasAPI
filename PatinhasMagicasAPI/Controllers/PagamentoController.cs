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
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public PagamentoController(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();

            if (!pagamentos.Any())
                return NotFound();

            var pagamentoOutputDTO = pagamentos.Select(p => new PagamentoOutputDTO
            {
                Id = p.Id,
                Data = p.DataPagamento,
                TipoPagamentoId = p.TipoPagamentoId,
                PedidoId = p.PedidoId,
            }).ToList();

            return Ok(pagamentoOutputDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);

            if (pagamento == null)
                return NotFound();

            var pagamentoDTO = new PagamentoOutputDTO
            {
                Id = pagamento.Id,
                Data = pagamento.DataPagamento,
                TipoPagamentoId = pagamento.TipoPagamentoId,
                PedidoId = pagamento.PedidoId,
            };

            return Ok(pagamentoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PagamentoInputDTO pagamentoInputDTO)
        {
            var pagamento = new Pagamento
            {
                DataPagamento = pagamentoInputDTO.Data,
                TipoPagamentoId = pagamentoInputDTO.TipoPagamentoId,
                PedidoId = pagamentoInputDTO.PedidoId
            };

            await _pagamentoRepository.AddAsync(pagamento);

            var pagamentoOutputDTO = new PagamentoOutputDTO
            {
                Id = pagamento.Id,
                Data = pagamento.DataPagamento,
                TipoPagamentoId = pagamento.TipoPagamentoId,
                PedidoId = pagamento.PedidoId,
            };

            return CreatedAtAction(nameof(GetById), new { id = pagamentoOutputDTO.Id }, pagamentoOutputDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PagamentoInputDTO pagamentoInputDTO)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);

            if (pagamento == null)
                return NotFound();

            pagamento = new Pagamento
            {
                Id = id,
                DataPagamento = pagamentoInputDTO.Data,
                TipoPagamentoId = pagamentoInputDTO.TipoPagamentoId,
                PedidoId = pagamentoInputDTO.PedidoId
            };

            await _pagamentoRepository.UpdateAsync(pagamento);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);
            if (pagamento == null)
                return NotFound();

            await _pagamentoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
