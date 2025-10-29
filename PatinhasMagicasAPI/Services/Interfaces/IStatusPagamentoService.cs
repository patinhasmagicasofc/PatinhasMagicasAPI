using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IStatusPagamentoService
    {
        Task<IEnumerable<StatusPagamento>> ObterTodosAsync();
        Task<StatusPagamento> ObterPorNomeAsync(string nome);
    }
}
