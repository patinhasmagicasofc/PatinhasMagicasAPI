using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByEmailAsync(string email);
        Task<Usuario> GetByCPFAsync(string cpf);
        IQueryable<Usuario> GetAllUsuarios();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task InativarAsync(Usuario usuario);
        Task ReativarAsync(Usuario usuario);

    }
}
