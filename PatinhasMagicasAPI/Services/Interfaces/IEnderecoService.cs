using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<List<EnderecoOutputDTO>> GetAsync();
        Task<EnderecoOutputDTO> GetByIdAsync(int id);
        Task AddAsync(EnderecoInputDTO enderecoInputDTO);
        Task UpdateAsync(int id, EnderecoInputDTO enderecoInputDTO);
        Task DeleteAsync(int id);

    }
}
