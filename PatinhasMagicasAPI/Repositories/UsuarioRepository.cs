using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using System.Threading.Tasks;


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
            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            }

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Usuario usuario)
        {
            var existingUsuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);

            if (existingUsuario == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {usuario.IdUsuario} não encontrado.");
            }

            if (!string.IsNullOrEmpty(usuario.Senha) && usuario.Senha != existingUsuario.Senha)
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            }
            else
            {
                usuario.Senha = existingUsuario.Senha;
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }


        public async Task<Usuario>? ValidarLoginAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios
                                        .Include(u => u.TipoUsuario)
                                        .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        // MÉTODOS DE CONSULTA

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).Include(u => u.Endereco).ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.TipoUsuario).Include(u => u.Endereco).FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        // MÉTODOS NÃO IMPLEMENTADOS

        public Task InativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReativarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}