using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IStatusPedidoRepository
    {
        Task<StatusPedido> GetByNameAsync(string nome);

    }
}
