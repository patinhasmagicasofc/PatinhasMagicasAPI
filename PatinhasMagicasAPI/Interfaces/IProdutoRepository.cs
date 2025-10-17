using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IProdutoRepository
    {
        Task AddAsync(Produto produto);
        Task<List<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
