using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;


namespace PatinhasMagicasAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        //campo de apoio
        private readonly PatinhasMagicasDbContext _context;

        //construtor com injeção de dependência
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

        //public async Task<List<Usuario>> GetAllAtivosAsync()
        //{
        //    return await _context.Usuarios.Where(u => u.Ativo).Include(u => u.TipoUsuario).ToListAsync();
        //}

        public async Task<Usuario> GetByIdAsync(int id)
        {
            //return await _context.Usuarios.Include(u => u.TipoUsuarioId).FirstOrDefaultAsync(u => u.IdUsuario == id);
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

        public Task UpdateAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario>? ValidarLoginAsync(string email, string senha)
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }
    }
}
