using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public AgendamentoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.AddAsync(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task<Agendamento> Add(Agendamento agendamento)
        {
            await _context.Agendamentos.AddAsync(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task<Agendamento> GetByIdAsync(int id)
        {
            return await _context.Agendamentos
                                  .Include(a => a.Animal)
                                      .ThenInclude(an => an.TamanhoAnimal)
                                  .Include(a => a.Animal)
                                      .ThenInclude(an => an.Especie)
                                  .Include(a => a.AgendamentoServicos)
                                      .ThenInclude(asrv => asrv.Servico)
                                          .ThenInclude(s => s.TipoServico)
                                  .Include(a => a.StatusAgendamento)
                                  .Include(a => a.Pedido)
                                      .ThenInclude(p => p.Pagamentos)
                                          .ThenInclude(pg => pg.TipoPagamento)
                                  .Include(a => a.Pedido)
                                      .ThenInclude(p => p.Pagamentos)
                                          .ThenInclude(pg => pg.StatusPagamento)
                                  .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Agendamento>> GetAllAsync()
        {
            return await _context.Agendamentos
                                  .Include(a => a.Animal)
                                      .ThenInclude(an => an.TamanhoAnimal)
                                  .Include(a => a.Animal)
                                      .ThenInclude(an => an.Especie)
                                  .Include(a => a.Animal)
                                      .ThenInclude(an => an.Usuario)
                                  .Include(a => a.AgendamentoServicos)
                                      .ThenInclude(asrv => asrv.Servico)
                                          .ThenInclude(s => s.TipoServico)
                                  .Include(a => a.StatusAgendamento)
                                  .Include(a => a.Pedido)
                                      .ThenInclude(p => p.Pagamentos)
                                          .ThenInclude(pg => pg.TipoPagamento)
                                  .Include(a => a.Pedido)
                                      .ThenInclude(p => p.Pagamentos)
                                          .ThenInclude(pg => pg.StatusPagamento)
                                  .ToListAsync();
        }

        public async Task UpdateAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Update(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
