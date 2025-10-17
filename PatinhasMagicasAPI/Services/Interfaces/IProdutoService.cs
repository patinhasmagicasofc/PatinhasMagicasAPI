using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IProdutoService
    {
        Task AddAsync(ProdutoInputDTO produtoInputDTO);
        Task<List<ProdutoOutputDTO>> GetAllAsync();
        Task<ProdutoOutputDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, ProdutoInputDTO produtoInputDTO);
        Task InativarAsync(int id);
        Task ReativarAsync(int id);
    }
}
