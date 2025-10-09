using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        //Task<List<Usuario>> GetAllAtivosAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByEmailAsync(string email);
        Task<Usuario> GetByCPFAsync(string cpf);

        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task InativarAsync(int id);
        Task ReativarAsync(int id);
        Task<Usuario>? ValidarLoginAsync(string email, string senha);

    }
}
