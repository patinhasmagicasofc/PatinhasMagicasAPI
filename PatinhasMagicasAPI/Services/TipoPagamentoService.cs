using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

public class TipoPagamentoService : ITipoPagamentoService
{
    private readonly ITipoPagamentoRepository _repository;

    public TipoPagamentoService(ITipoPagamentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TipoPagamento>> ObterTodosAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TipoPagamento> ObterPorIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
