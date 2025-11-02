using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaOuputDTO>>> GetAll()
        {
            var categorias = await _categoriaRepository.GetAllAsync();

            if (!categorias.Any())
                return NotFound();
            
            return Ok(_mapper.Map<IEnumerable<CategoriaOuputDTO>>(categorias));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaOuputDTO>> GetById(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(_mapper.Map<CategoriaOuputDTO>(categoria));
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<ActionResult<CategoriaOuputDTO>> Post(CategoriaInputDTO categoriaInput)
        {
            var categoria = _mapper.Map<Categoria>(categoriaInput);
            await _categoriaRepository.AddAsync(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, _mapper.Map<CategoriaOuputDTO>(categoria));
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoriaInputDTO categoriaInput)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(id);
            if (existingCategoria == null)
                return NotFound();

            await _categoriaRepository.UpdateAsync(existingCategoria);
            return NoContent();
        }


        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
                return NotFound();

            await _categoriaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}