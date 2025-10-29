using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

public class StatusPagamentoService : IStatusPagamentoService
{
    private readonly IStatusPagamentoRepository _repository;

    public StatusPagamentoService(IStatusPagamentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StatusPagamento>> ObterTodosAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<StatusPagamento> ObterPorNomeAsync(string nome)
    {
        return await _repository.GetByNameAsync(nome);
    }
}
