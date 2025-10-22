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
            return await _context.AgendamentoServico.ToListAsync();
        }

        public async Task<List<AgendamentoServico>> GetServicosByAgendamentoIdAsync(int agendamentoId)
        {
            return await _context.AgendamentoServico
                .Include(asrv => asrv.Servico)  
                .Where(asrv => asrv.AgendamentoId == agendamentoId)
                .ToListAsync();
        }

        public async Task<AgendamentoServico> GetByIdAsync(int id)
        {
            return await _context.AgendamentoServico.FindAsync(id);
        }

        public async Task AddAsync(AgendamentoServico agendamentoServico)
        {
            await _context.AgendamentoServico.AddAsync(agendamentoServico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AgendamentoServico agendamentoServico)
        {
            _context.Entry(agendamentoServico).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var agendamentoServico = await _context.AgendamentoServico.FindAsync(id);
            if (agendamentoServico != null)
            {
                _context.AgendamentoServico.Remove(agendamentoServico);
                await _context.SaveChangesAsync();
            }
        }
    }
}

