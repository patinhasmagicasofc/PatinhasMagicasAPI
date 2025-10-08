using PatinhasMagicasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IEnderecoRepository
    {
        
        Task<List<Endereco>> GetAsync();

        Task<Endereco> GetByIdAsync(int id);
        Task<Endereco> GetEnderecoExistenteAsync(int usuarioId, string cep, string logradouro, string bairro, string cidade, string estado);


        Task AddAsync(Endereco endereco);
        Task UpdateAsync(Endereco endereco);
        Task DeleteAsync(Endereco endereco);

        // Método para persistir as mudanças no banco de dados
        Task<bool> SaveChangesAsync();
    }
}
