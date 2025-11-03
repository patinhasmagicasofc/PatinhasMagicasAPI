using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProdutoOutputDTO>>> GetProdutos()
       {
            var produtoOutputDTOs = await _produtoService.GetAllAsync();
            if (!produtoOutputDTOs.Any())
                return Ok(new { success = true, message = "Nenhum usuario encontrado!", produtos = new List<ProdutoOutputDTO>() });

            return Ok(produtoOutputDTOs);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProdutoOutputDTO>> GetProduto(int id)
        {
            var produtoOutputDTO = await _produtoService.GetByIdAsync(id);

            if (produtoOutputDTO == null) return NotFound(new { message = "Produto não encontrado." });

            return Ok(produtoOutputDTO);
        }

        [Authorize(Roles = "Administrador,Funcionario")]
        [HttpPost]
        public async Task<ActionResult<ProdutoOutputDTO>> Post(ProdutoInputDTO produtoInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _produtoService.AddAsync(produtoInputDTO);

            return Ok(new { success = true, message = "Produto cadastrado com sucesso!", produtoInputDTO });
        }

        [Authorize(Roles = "Administrador,Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoInputDTO produtoInputDTO  )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _produtoService.UpdateAsync(id, produtoInputDTO);
            return Ok(new { success = true, message = "Produto atualizado com sucesso!", produtoInputDTO });
        }

        [Authorize(Roles = "Administrador,Funcionario")]
        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarProduto(int id)
        {
            await _produtoService.InativarAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Administrador,Funcionario")]
        [HttpPut("reativar/{id}")]
        public async Task<IActionResult> ReativarProduto(int id)
        {
            await _produtoService.ReativarAsync(id);
            return NoContent();
        }

    }
}