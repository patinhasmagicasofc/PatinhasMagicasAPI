using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface ITipoPagamentoService
    {
        Task<IEnumerable<TipoPagamento>> ObterTodosAsync();
        Task<TipoPagamento> ObterPorIdAsync(int id);
    }
}
