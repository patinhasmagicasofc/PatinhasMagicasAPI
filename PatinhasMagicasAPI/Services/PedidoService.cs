using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using System.Threading.Tasks;

namespace PatinhasMagicasAPI.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoRepository _pagamentoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IPagamentoRepository pagamentoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoRepository = pagamentoRepository;
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

        public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
        {
            // Adiciona o pedido ao repositório

            var pagamento = _pagamentoRepository.GetByIdAsync(pedido.Id);

            var teste = _pagamentoRepository.ExistsByPedidoId(pedido.Id);

            if(teste == null)
                pedido.StatusPedidoId = 1; // Define o status inicial do pedido (ex: AguardandoPagamento)

            if (!pedido.Pagamentos.Any())
                pedido.StatusPedidoId = 1; // Define o status inicial do pedido (ex: AguardandoPagamento)

            await _pedidoRepository.AddAsync(pedido);

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
            return pedido;
        }

        public decimal GetValorTotalPedido(Pedido pedido)
        {
            return pedido.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade);
        }

        public string GetFormaPagamento(Pedido pedido)
        {
            var formaPagamento = pedido.Pagamentos.Select(p => p.TipoPagamento?.Metodo).FirstOrDefault();
            return formaPagamento ?? "Pendente";
        }

        public int GetTotalPedidosHoje(Pedido pedido)
        {
            return _pedidoRepository.GetTotalPedidosHoje(pedido);
        }

        public decimal GetTotalVendasHoje(Pedido pedido)
        {
           return _pedidoRepository.GetTotalVendasHoje(pedido);
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetByIdAsync(id);
        }
        
        public async Task<List<Pedido>> GetAllPedidosAsync()
        {
            return await _pedidoRepository.GetAllAsync();
        }
        public async Task UpdatePedidoAsync(Pedido pedido)
        {
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task DeletePedidoAsync(int id)
        {
            await _pedidoRepository.DeleteAsync(id);
        }

    }
}
