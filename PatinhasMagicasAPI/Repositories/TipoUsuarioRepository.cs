using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public TipoUsuarioRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoUsuario>> GetAllAsync()
        {
            return await _context.TiposUsuarios.ToListAsync();
        }

        public async Task<TipoUsuario> GetByIdAsync(int id)
        {
            return await _context.TiposUsuarios.FindAsync(id);
        }

        public async Task AddAsync(TipoUsuario tipoUsuario)
        {
            await _context.TiposUsuarios.AddAsync(tipoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TipoUsuario tipoUsuario)
        {
            _context.Entry(tipoUsuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tipoUsuario = await _context.TiposUsuarios.FindAsync(id);
            if (tipoUsuario != null)
            {
                _context.TiposUsuarios.Remove(tipoUsuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TipoUsuario?> GetByNomeAsync(string nome)
        {
            return await _context.TiposUsuarios
                .FirstOrDefaultAsync(t => t.Nome.ToLower() == nome.ToLower());
        }
    }
}