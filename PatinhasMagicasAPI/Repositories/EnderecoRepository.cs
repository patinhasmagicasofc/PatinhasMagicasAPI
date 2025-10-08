using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public EnderecoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        // Renomeado para GetAsync() conforme a interface
        public async Task<List<Endereco>> GetAsync()
        {
            return await _context.Enderecos.ToListAsync();
        }

        public async Task<Endereco> GetByIdAsync(int id)
        {
            return await _context.Enderecos.FindAsync(id);
        }

        public async Task AddAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
        }

        public async Task UpdateAsync(Endereco endereco)
        {

            var existingEndereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.IdEndereco == endereco.IdEndereco);

            if (existingEndereco == null)
            {
                throw new KeyNotFoundException($"Endereço com ID {endereco.IdEndereco} não encontrado.");
            }

            _context.Entry(existingEndereco).CurrentValues.SetValues(endereco);

            await Task.CompletedTask;
        }

        public Task DeleteAsync(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
            return Task.CompletedTask;
        }

        // MÉTODO CRÍTICO: Persiste as mudanças no banco.
        // Deve ser chamado pelo Controller ou Service após as operações Add/Update/Delete.
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}