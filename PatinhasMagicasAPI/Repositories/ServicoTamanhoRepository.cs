using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class ServicoTamanhoRepository : IServicoTamanhoRepository
    {

        private readonly PatinhasMagicasDbContext _context;

        public ServicoTamanhoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<ServicoTamanho?> GetByServicoAndTamanhoAsync(int idServico, int idTamanhoAnimal)
        {
            return await _context.ServicoTamanhos
                .Where(st => st.ServicoId == idServico && st.TamanhoAnimalId == idTamanhoAnimal)
                .FirstOrDefaultAsync();
        }
    }
}
