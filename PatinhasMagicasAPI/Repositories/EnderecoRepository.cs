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
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }

        
    }
}