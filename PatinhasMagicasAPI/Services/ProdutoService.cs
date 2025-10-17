using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task AddAsync(ProdutoInputDTO produtoInputDTO)
        {
            var produto = _mapper.Map<Produto>(produtoInputDTO);


            await _produtoRepository.AddAsync(produto);
        }

        public async Task<List<ProdutoOutputDTO>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<List<ProdutoOutputDTO>>(produtos);
        }

        public async Task<ProdutoOutputDTO> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return _mapper.Map<ProdutoOutputDTO>(produto);
        }

        public async Task UpdateAsync(int id, ProdutoInputDTO produtoInputDTO)
        {
            var produto = _mapper.Map<Produto>(produtoInputDTO);
            produto.Id = id;

            var produtoExiste = await _produtoRepository.GetByIdAsync(id);
            if (produtoExiste == null)
                throw new KeyNotFoundException("Produto  não encontrado.");

            await _produtoRepository.UpdateAsync(produto);
        }

        private List<ProdutoOutputDTO> Map(List<Produto> produtos)
        {
            var produtosDTO = produtos.Select(p => new ProdutoOutputDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                CategoriaId = p.CategoriaId
            }).ToList();

            return produtosDTO;
        }

        public Task InativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReativarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
