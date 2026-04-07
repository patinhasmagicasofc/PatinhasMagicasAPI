using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IPasskeyCredentialRepository
    {
        Task<List<PasskeyCredential>> GetByUsuarioIdAsync(int usuarioId);
        Task<PasskeyCredential?> GetByIdAsync(int id);
        Task<PasskeyCredential?> GetByCredentialIdAsync(string credentialId);
        Task AddAsync(PasskeyCredential credential);
        Task UpdateAsync(PasskeyCredential credential);
        Task DeleteAsync(PasskeyCredential credential);
    }
}
