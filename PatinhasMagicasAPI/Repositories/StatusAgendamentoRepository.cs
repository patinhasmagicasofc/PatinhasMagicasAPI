using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class StatusAgendamentoRepository : IStatusAgendamentoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public StatusAgendamentoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StatusAgendamento statusAgendamento)
        {
            await _context.StatusAgendamentos.AddAsync(statusAgendamento);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StatusAgendamento>> GetAllAsync()
        {
            return await _context.StatusAgendamentos.ToListAsync();
        }

        public async Task<StatusAgendamento> GetByIdAsync(int id)
        {
            return await _context.StatusAgendamentos.FindAsync(id);
        }

        public async Task UpdateAsync(StatusAgendamento statusAgendamento)
        {
            _context.StatusAgendamentos.Update(statusAgendamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var statusAgendamento = await _context.StatusAgendamentos.FindAsync(id);
            if (statusAgendamento != null)
            {
                _context.StatusAgendamentos.Remove(statusAgendamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
