using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ITipoPagamentoRepository _tipoPagamentoRepository;
    private readonly IStatusPagamentoRepository _statusPagamentoRepository;

    public PagamentoService(
        IPagamentoRepository pagamentoRepository,
        ITipoPagamentoRepository tipoPagamentoRepository,
        IStatusPagamentoRepository statusPagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
        _tipoPagamentoRepository = tipoPagamentoRepository;
        _statusPagamentoRepository = statusPagamentoRepository;
    }

    public async Task<Pagamento> CriarPagamentoAsync(Pagamento pagamento)
    {
        var tipo = await _tipoPagamentoRepository.GetByIdAsync(pagamento.TipoPagamentoId);
        if (tipo == null)
            throw new Exception("Tipo de pagamento inválido.");

        string statusInicial = DefinirStatusInicial(tipo.Nome);
        var status = await _statusPagamentoRepository.GetByNameAsync(statusInicial);
        if (status == null)
            throw new Exception("Status não encontrado.");

        pagamento.StatusPagamentoId = status.Id;
        pagamento.DataPagamento = DateTime.Now;

        return await _pagamentoRepository.AddAsync(pagamento);
    }

    private string DefinirStatusInicial(string nomeTipo)
    {
        nomeTipo = nomeTipo.ToLower();

        if (nomeTipo.Contains("crédito") || nomeTipo.Contains("débito") || nomeTipo.Contains("pix"))
            return "Aprovado";

        return "Pendente";
    }
}
