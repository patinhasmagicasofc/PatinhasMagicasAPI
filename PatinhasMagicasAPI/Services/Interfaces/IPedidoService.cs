using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoOutputDTO>> GetAllAsync();
        Task<PedidoOutputDTO> GetByIdAsync(int id);
        Task<List<PedidoOutputDTO>> GetPedidosByUsuarioId(int id);
        Task<DashboardPedidoDTO> GetPedidosPaginados(PedidoFiltroDTO filtro);
        Task<PedidoOutputDTO> CreatePedidoAsync(PedidoCompletoInputDTO pedidoInputDTO);
        Task Update(int id, PedidoUpdateDTO dto);
    }
}

