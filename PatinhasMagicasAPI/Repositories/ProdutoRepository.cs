using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly PatinhasMagicasDbContext _context;
        public ProdutoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Produto produto)
        {
            _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.Include(p => p.Categoria).FirstAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
