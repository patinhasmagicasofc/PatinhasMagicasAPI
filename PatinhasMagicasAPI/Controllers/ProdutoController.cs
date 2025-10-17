using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Services;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoOutputDTO>>> GetProdutos()
        {
            var produtoOutputDTOs = await _produtoService.GetAllAsync();
            if (!produtoOutputDTOs.Any())
                return Ok(new { success = true, message = "Nenhum usuario encontrado!", produtos = new List<ProdutoOutputDTO>() });

            return Ok(produtoOutputDTOs);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoOutputDTO>> GetProduto(int id)
        {
            var produtoOutputDTO = await _produtoService.GetByIdAsync(id);

            if (produtoOutputDTO == null) return NotFound(new { message = "Produto não encontrado." });

            return Ok(produtoOutputDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoOutputDTO>> Post(ProdutoInputDTO produtoInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _produtoService.AddAsync(produtoInputDTO);

            //return CreatedAtAction(nameof(GetProduto), new { id = produto.IdProduto }, produto);
            return Ok(new { success = true, message = "Produto cadastrado com sucesso!", produtoInputDTO });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoInputDTO produtoInputDTO  )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _produtoService.UpdateAsync(id, produtoInputDTO);
            //return NoContent();
            return Ok(new { success = true, message = "Produto atualizado com sucesso!" });
        }

        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarProduto(int id)
        {
            await _produtoService.InativarAsync(id);
            return NoContent();
        }

        [HttpPut("reativar/{id}")]
        public async Task<IActionResult> ReativarProduto(int id)
        {
            await _produtoService.ReativarAsync(id);
            return NoContent();
        }

    }
}