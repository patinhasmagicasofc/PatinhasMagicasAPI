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
            return await _context.Usuarios.AsNoTracking().Include(u => u.TipoUsuario).Include(u => u.Endereco).ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.AsNoTracking().Include(u => u.TipoUsuario).Include(u => u.Endereco).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> GetByCPFAsync(string cpf)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CPF == cpf);
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).FirstOrDefaultAsync(u => u.Email == email);
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
            var local = _context.Usuarios.Local.FirstOrDefault(u => u.Id == usuario.Id);
            if (local != null)
            {
                // Desanexar a versão antiga da entidade
                _context.Entry(local).State = EntityState.Detached;
            }

            // Anexar a nova versão da entidade
            _context.Attach(usuario);
            _context.Entry(usuario).State = EntityState.Modified;

            // Salvar as alterações no banco
            _context.SaveChanges();
        }

    }
}