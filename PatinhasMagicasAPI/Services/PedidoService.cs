using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository, IPagamentoRepository pagamentoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoRepository = pagamentoRepository;
            _mapper = mapper;
        }

        // --- GET BY ID ---
        public async Task<PedidoOutputDTO> GetByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            return _mapper.Map<PedidoOutputDTO>(pedido);
        }

        // --- GET PEDIDOS POR USUÁRIO ---
        public async Task<List<PedidoOutputDTO>> GetPedidosByUsuarioId(int id)
        {
            var pedidos = await _pedidoRepository.GetPedidosByUsuarioId(id);
            return _mapper.Map<List<PedidoOutputDTO>>(pedidos);
        }

        // --- CRIA PEDIDO ---
        public async Task<PedidoOutputDTO> CreatePedidoAsync(PedidoInputDTO pedidoInputDTO)
        {
            var pedido = _mapper.Map<Pedido>(pedidoInputDTO);

            // Define status inicial caso não haja pagamentos
            pedido.StatusPedidoId = 1;

            await _pedidoRepository.AddAsync(pedido);

            return _mapper.Map<PedidoOutputDTO>(pedido);
        }

        // --- DASHBOARD / PEDIDOS PAGINADOS ---
        public async Task<DashboardPedidoDTO> GetPedidosPaginados(PedidoFiltroDTO filtro)
        {
            if (filtro.DataInicio == null || filtro.DataFim == null)
            {
                var hoje = DateTime.Today;
                filtro.DataInicio = hoje;
                filtro.DataFim = hoje;
            }

            var query = _pedidoRepository.GetAllPedidos();

            query = FiltrarPorData(query, filtro.DataInicio, filtro.DataFim);

            if (!string.IsNullOrEmpty(filtro.Nome))
                query = FiltrarPorNome(query, filtro.Nome);

            if (!string.IsNullOrEmpty(filtro.Status))
                query = FiltrarPorStatus(query, filtro.Status);

            var total = await query.CountAsync();

            var pedidos = await query
                .OrderByDescending(p => p.DataPedido)
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToListAsync();

            var dashboardPedido = new DashboardPedidoDTO
            {
                PedidoOutputDTO = _mapper.Map<List<PedidoOutputDTO>>(pedidos),
                QTotalVendas = total,
                ValorTotalVendas = pedidos.Sum(p => p.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade)),
                QPedidosCancelado = pedidos.Count(p => p.StatusPedido.Nome == "Cancelado"),
                QPedidosPendente = pedidos.Count(p => p.StatusPedido.Nome == "Pendente")
            };

            return dashboardPedido;
        }

        // --- FILTROS ---
        private IQueryable<Pedido> FiltrarPorData(IQueryable<Pedido> query, DateTime? inicio, DateTime? fim)
        {
            if (inicio == null || fim == null) return query;
            var fimMaisUmDia = fim.Value.Date.AddDays(1);
            return query.Where(p => p.DataPedido >= inicio.Value.Date && p.DataPedido < fimMaisUmDia);
        }

        private IQueryable<Pedido> FiltrarPorNome(IQueryable<Pedido> query, string nome)
        {
            return query.Where(p => p.Usuario.Nome.Contains(nome));
        }

        private IQueryable<Pedido> FiltrarPorStatus(IQueryable<Pedido> query, string status)
        {
            return query.Where(p => p.StatusPedido.Nome == status);
        }
    }
}
