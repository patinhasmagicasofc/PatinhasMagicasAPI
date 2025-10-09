using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IEnderecoRepository
    {
        
        Task<List<Endereco>> GetAsync();
        Task<Endereco> GetByIdAsync(int id);
        Task AddAsync(Endereco endereco);
        Task UpdateAsync(Endereco endereco);
        Task DeleteAsync(Endereco idendereco);
    }
}
