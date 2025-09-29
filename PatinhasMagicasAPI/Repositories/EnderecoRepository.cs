using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    // O repositório implementa a interface IEnderecoRepository para gerenciar as operações de persistência
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        // Construtor para injeção de dependência do DbContext
        public EnderecoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        // Obtém a lista completa de endereços de forma assíncrona
        public async Task<List<Endereco>> GetAsync()
        {
            // Retorna a lista de endereços diretamente, sem DTO mapping, conforme a interface
            return await _context.Enderecos.ToListAsync();
        }

        // Obtém um endereço específico pelo ID de forma assíncrona
        public async Task<Endereco> GetByIdAsync(int id)
        {
            // FindAsync é o método mais eficiente para buscar pela chave primária
            return await _context.Enderecos.FindAsync(id);
        }

        // Adiciona um novo endereço ao contexto de forma assíncrona
        public async Task AddAsync(Endereco endereco)
        {
            await _context.AddAsync(endereco);
        }

        // Atualiza uma entidade Endereco no contexto
        public Task UpdateAsync(Endereco endereco)
        {
            // Marca a entidade como modificada.
            _context.Entry(endereco).State = EntityState.Modified;
            // Retorna Task.CompletedTask pois a operação não acessa o banco diretamente (o SaveChangesAsync fará isso)
            return Task.CompletedTask;
        }

        // Remove uma entidade Endereco do contexto
        public Task DeleteAsync(Endereco endereco)
        {
            // Marca a entidade para remoção.
            _context.Enderecos.Remove(endereco);
            // Retorna Task.CompletedTask pelo mesmo motivo acima.
            return Task.CompletedTask;
        }

        // Persiste todas as mudanças pendentes (adição, atualização, remoção) no banco de dados
        public async Task<bool> SaveChangesAsync()
        {
            // Retorna true se uma ou mais linhas foram afetadas no banco
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
