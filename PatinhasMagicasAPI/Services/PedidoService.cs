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

        public async Task<PedidoOutputDTO> GetByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);

            var pedidoOutputDTO = Map(pedido);

            ///return _mapper.Map<PedidoOutputDTO>(pedido);
            return pedidoOutputDTO;
        }

        public async Task<List<PedidoOutputDTO>> GetPedidosByUsuarioId(int id)
        {
            var pedidos = await _pedidoRepository.GetPedidosByUsuarioId(id);

            if (pedidos == null || !pedidos.Any())
                return new List<PedidoOutputDTO>();

            var pedidosDto = pedidos.Select(p => new PedidoOutputDTO
            {
                Id = p.Id,
                DataPedido = p.DataPedido,
                UsuarioId = p.UsuarioId,
                NomeUsuario = p.Usuario.Nome,
                StatusPedidoId = p.StatusPedidoId,
                StatusPedido = p.StatusPedido.Nome,
                StatusPagamento = p.Pagamentos.FirstOrDefault()?.StatusPagamento?.Nome ?? "Indisponivel",
                ValorPedido = p.ItensPedido.Sum(i => (decimal?)(i.Quantidade * i.PrecoUnitario)) ?? 0,
                UsuarioOutputDTO = p.Usuario is null ? null : new UsuarioOutputDTO
                {
                    Nome = p.Usuario.Nome,
                    Email = p.Usuario.Email,
                    Endereco = p.Usuario.Endereco is null ? null : new EnderecoOutputDTO
                    {
                        Logradouro = p.Usuario.Endereco.Logradouro,
                        Numero = p.Usuario.Endereco.Numero,
                        Cidade = p.Usuario.Endereco.Cidade,
                        Estado = p.Usuario.Endereco.Estado,
                        CEP = p.Usuario.Endereco.CEP,
                        Complemento = p.Usuario.Endereco.Complemento,
                        Bairro = p.Usuario.Endereco.Bairro,
                    }
                },

                ItemPedidoOutputDTOs = p.ItensPedido.Select(i => new ItemPedidoOutputDTO
                {
                    Produto = i.Produto.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,

                    ProdutoOutputDTO = new ProdutoOutputDTO
                    {
                        UrlImagem = i.Produto.UrlImagem
                    }


                }).ToList()
            }).ToList();

            return pedidosDto;
        }

        //public async Task AtualizarStatusPedidoAsync(int pedidoId)
        //{
        //    var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
        //    if (pedido == null)
        //        throw new Exception("Pedido não encontrado.");

        //    var pagamentos = await _pagamentoRepository
        //        .GetAllAsync(p => p.PedidoId == pedidoId && p.StatusPagamento == StatusPagamento.Aprovado);

        //    var totalPago = pagamentos.Sum(p => p.Valor);

        //    if (totalPago >= pedido.ValorTotal)
        //        pedido.StatusPedido.Nome == "Pago";
        //    else if (totalPago > 0)
        //        pedido.StatusPedido.Nome == "ParcialmentePago";
        //    else
        //        pedido.StatusPedido.Nome == "AguardandoPagamento";

        //    await _pedidoRepository.UpdateAsync(pedido);
        //}

        public async Task<DashboardPedidoDTO> GetPedidosPaginados(PedidoFiltroDTO filtro)
        {
            if (filtro.DataInicio == null || filtro.DataFim == null)
            {
                var hoje = DateTime.Today;
                filtro.DataInicio = hoje;
                filtro.DataFim = hoje;
            }

            var query = _pedidoRepository.GetAllPedidos();

            // Aplicando filtros apenas se existirem
            query = FiltrarPorData(query, filtro.DataInicio, filtro.DataFim);

            if (!string.IsNullOrEmpty(filtro.Nome))
                query = FiltrarPorNome(query, filtro.Nome);

            if (!string.IsNullOrEmpty(filtro.Status))
                query = FiltrarPorStatus(query, filtro.Status);

            // Total antes da paginação
            var total = await query.CountAsync();

            var pedidos = await query
                .OrderByDescending(p => p.DataPedido)
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToListAsync();

            var dashboardPedido = new DashboardPedidoDTO();
            var pedidoOutputDTOs = Map(pedidos);

            dashboardPedido.PedidoOutputDTO = pedidoOutputDTOs;
            dashboardPedido.QTotalVendas = total;
            dashboardPedido.ValorTotalVendas = GetTotalVendas(pedidos);
            dashboardPedido.QPedidosCancelado = GetPedidosCancelados(pedidos);
            dashboardPedido.QPedidosPendente = GetPedidosPendentes(pedidos);

            return dashboardPedido;
        }

        private List<PedidoOutputDTO> Map(List<Pedido> pedidos)
        {
            var pedidosDTO = pedidos.Select(p => new PedidoOutputDTO
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                DataPedido = p.DataPedido,
                StatusPedidoId = p.StatusPedidoId,
                StatusPedido = p.StatusPedido.Nome,
                NomeUsuario = p.Usuario?.Nome,
                ValorPedido = GetValorPedido(p),
                FormaPagamento = GetFormaPagamento(p),
                StatusPagamento = p.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault()
            }).ToList();

            return pedidosDTO;
        }

        private PedidoOutputDTO Map(Pedido p)
        {
            var pedidosDTO = new PedidoOutputDTO
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                DataPedido = p.DataPedido,
                StatusPedidoId = p.StatusPedidoId,
                StatusPedido = p.StatusPedido.Nome,
                NomeUsuario = p.Usuario?.Nome,
                ValorPedido = GetValorPedido(p),
                FormaPagamento = GetFormaPagamento(p),
                StatusPagamento = p.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault(),

                ItemPedidoOutputDTOs = p.ItensPedido.Select(i => new ItemPedidoOutputDTO
                {
                    Produto = i.Produto.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,

                    ProdutoOutputDTO = new ProdutoOutputDTO
                    {
                        UrlImagem = i.Produto.UrlImagem
                    }

                }).ToList()

            };

            return pedidosDTO;
        }

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

        public async Task<PedidoOutputDTO> CreatePedidoAsync(PedidoInputDTO pedidoInputDTO)
        {
            var pedido = _mapper.Map<Pedido>(pedidoInputDTO);
            // Adiciona o pedido ao repositório

            //var pagamento = await _pagamentoRepository.GetByIdAsync(pedido.Id);

            //var teste = await _pagamentoRepository.ExistsByPedidoId(pedido.Id);

            //if (teste == null)
            pedido.StatusPedidoId = 1; // Define o status inicial do pedido (ex: AguardandoPagamento)

            if (!pedido.Pagamentos.Any())
                pedido.StatusPedidoId = 1; // Define o status inicial do pedido (ex: AguardandoPagamento)

            await _pedidoRepository.AddAsync(pedido);

            var pedidoOutputDTO = _mapper.Map<PedidoOutputDTO>(pedido);

            //await _pedidoRepository.AddAsync(pedido);
            //// Cria um pagamento associado ao pedido
            //var pagamento = new Pagamento
            //{
            //    PedidoId = pedido.Id,
            //    Valor = pedido.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade), // Calcula o valor total do pedido
            //    Data = DateTime.Now,
            //    StatusPagamentoId = 1 // Define o status inicial do pagamento (ex: Pendente)
            //};
            //// Adiciona o pagamento ao repositório
            //await _pagamentoRepository.AddAsync(pagamento);
            return pedidoOutputDTO;
        }

        public decimal GetValorPedido(Pedido pedido)
        {
            return pedido.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade);
        }

        public string GetFormaPagamento(Pedido pedido)
        {
            var formaPagamento = pedido.Pagamentos.Select(p => p.TipoPagamento?.Nome).FirstOrDefault();
            return formaPagamento ?? "Pendente";
        }

        public decimal GetTotalVendas(List<Pedido> pedidos)
        {
            var totalVendas = pedidos.Sum(p => p.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade));

            return totalVendas;
        }

        public int GetPedidosCancelados(List<Pedido> pedidos)
        {
            var totalPedidosCancelados = pedidos.Count(p => p.StatusPedido.Nome == "Cancelado");
            return totalPedidosCancelados;
        }
        public int GetPedidosPendentes(List<Pedido> pedidos)
        {
            var totalPedidosPendentes = pedidos.Count(p => p.StatusPedido.Nome == "Pendente");
            return totalPedidosPendentes;
        }

    }
}
