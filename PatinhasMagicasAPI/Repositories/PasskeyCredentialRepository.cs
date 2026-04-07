using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class PasskeyCredentialRepository : IPasskeyCredentialRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public PasskeyCredentialRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<List<PasskeyCredential>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.PasskeyCredentials
                .AsNoTracking()
                .Where(credential => credential.UsuarioId == usuarioId)
                .OrderByDescending(credential => credential.CreatedAt)
                .ToListAsync();
        }

        public async Task<PasskeyCredential?> GetByIdAsync(int id)
        {
            return await _context.PasskeyCredentials.FirstOrDefaultAsync(credential => credential.Id == id);
        }

        public async Task<PasskeyCredential?> GetByCredentialIdAsync(string credentialId)
        {
            return await _context.PasskeyCredentials.FirstOrDefaultAsync(credential => credential.CredentialId == credentialId);
        }

        public async Task AddAsync(PasskeyCredential credential)
        {
            await _context.PasskeyCredentials.AddAsync(credential);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PasskeyCredential credential)
        {
            _context.PasskeyCredentials.Update(credential);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PasskeyCredential credential)
        {
            _context.PasskeyCredentials.Remove(credential);
            await _context.SaveChangesAsync();
        }
    }
}
