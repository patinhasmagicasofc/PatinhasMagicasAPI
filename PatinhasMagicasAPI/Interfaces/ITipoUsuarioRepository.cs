using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        Task<List<TipoUsuario>> GetAllAsync();
        Task<TipoUsuario> GetByIdAsync(int id);
        Task<TipoUsuario?> GetByNomeAsync(string nome);
        Task AddAsync(TipoUsuario tipoUsuario);
        Task UpdateAsync(TipoUsuario tipoUsuario);
        Task DeleteAsync(int id);
    }
}
