using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;


namespace PatinhasMagicasAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly PatinhasMagicasDbContext _context;

        public UsuarioRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).Include(u=>u.Endereco).ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).Include(u => u.Endereco).FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public Task InativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario>? ValidarLoginAsync(string email, string senha)
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }
    }
}
