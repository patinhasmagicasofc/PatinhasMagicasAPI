using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioOutputDTO>> GetAllAsync();
        Task<UsuarioOutputDTO> GetByIdAsync(int id);
        Task AddAsync(UsuarioInputDTO usuarioInputDTO);
        Task UpdateAsync(int id, UsuarioUpdateDTO usuarioUpdateDTO);
        Task ReativarAsync(int id);
        Task InativarAsync(int id);
        Task<LoginUsuarioOutputDTO>? ValidarLoginAsync(string email, string senha);
    }
}
