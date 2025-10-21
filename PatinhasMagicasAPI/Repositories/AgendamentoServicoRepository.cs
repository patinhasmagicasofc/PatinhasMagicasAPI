using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class AgendamentoServicoRepository : IAgendamentoServicoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public AgendamentoServicoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<List<AgendamentoServico>> GetAllAsync()
        {
            return await _context.AgendamentoServicos.ToListAsync();
        }

        public async Task<AgendamentoServico> GetByIdAsync(int id)
        {
            return await _context.AgendamentoServicos.FindAsync(id);
        }

        public async Task AddAsync(AgendamentoServico agendamentoServico)
        {
            await _context.AgendamentoServicos.AddAsync(agendamentoServico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AgendamentoServico agendamentoServico)
        {
            _context.Entry(agendamentoServico).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var agendamentoServico = await _context.AgendamentoServicos.FindAsync(id);
            if (agendamentoServico != null)
            {
                _context.AgendamentoServicos.Remove(agendamentoServico);
                await _context.SaveChangesAsync();
            }
        }
    }
}

