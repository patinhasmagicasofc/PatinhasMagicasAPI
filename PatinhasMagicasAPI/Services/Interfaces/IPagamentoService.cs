using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IPagamentoService
    {
        Task<Pagamento> CriarPagamentoAsync(Pagamento pagamento);
    }
}
