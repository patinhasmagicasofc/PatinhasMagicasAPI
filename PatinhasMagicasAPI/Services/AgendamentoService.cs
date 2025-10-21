using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IServicoTamanhoRepository _servicoTamanhoRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IAgendamentoServicoRepository _agendamentoServicoRepository;
        private readonly IMapper _mapper;

        public AgendamentoService(
            IPedidoRepository pedidoRepository,
            IAgendamentoRepository agendamentoRepository,
            IServicoTamanhoRepository servicoTamanhoRepository,
            IAnimalRepository animalRepository,
            IAgendamentoServicoRepository agendamentoServicoRepository,
            IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _agendamentoRepository = agendamentoRepository;
            _servicoTamanhoRepository = servicoTamanhoRepository;
            _animalRepository = animalRepository;
            _agendamentoServicoRepository = agendamentoServicoRepository;
            _mapper = mapper;
        }

        public async Task<AgendamentoOutputDTO> CriarAgendamentoAsync(AgendamentoCreateDTO agendamentoCreateDTO)
        {
            //using var transaction = await (_agendamentoRepository as AgendamentoRepository)!._context.Database.BeginTransactionAsync();

            try
            {
                var animal = await _animalRepository.GetByIdAsync(agendamentoCreateDTO.IdAnimal);
                if (animal == null) throw new Exception("Animal não encontrado");

                decimal valorTotal = 0;
                var agendamentoServicoDTOs = new List<AgendamentoServicoDTO>();

                foreach (var servicoId in agendamentoCreateDTO.IdServicos)
                {
                    var servicoTamanho = await _servicoTamanhoRepository.GetByServicoAndTamanhoAsync(servicoId, animal.TamanhoAnimalId);
                    if (servicoTamanho == null) throw new Exception($"Serviço {servicoId} não disponível para este animal");

                    valorTotal += servicoTamanho.Preco;
                    agendamentoServicoDTOs.Add(new AgendamentoServicoDTO { Id = servicoId, Preco = servicoTamanho.Preco });
                }

                var pedido = await _pedidoRepository.Add(new Pedido
                {
                    UsuarioId = agendamentoCreateDTO.IdUsuario ?? 0,
                    StatusPedidoId = 1,
                    DataPedido = DateTime.Now
                });


                var agendamento = await _agendamentoRepository.Add(new Agendamento
                {
                    AnimalId = agendamentoCreateDTO.IdAnimal,
                    PedidoId = pedido.Id,
                    StatusAgendamentoId = 1,
                    DataAgendamento = agendamentoCreateDTO.DataAgendamento ?? DateTime.Now,
                    DataCadastro = DateTime.Now
                });

                var agendamentoServicos = agendamentoServicoDTOs.Select(s => new AgendamentoServico
                {
                    AgendamentoId = agendamento.Id,
                    ServicoId = s.Id,
                    Preco = s.Preco
                }).ToList();

                foreach (var agendamentoServico  in agendamentoServicos)
                {
                    await _agendamentoServicoRepository.AddAsync(agendamentoServico);
                }

                // transaction.CommitAsync();

                return new AgendamentoOutputDTO
                {
                    Id = agendamento.Id,
                    PedidoId = pedido.Id,
                    DataAgendamento = agendamento.DataAgendamento,
                    ValorTotal = valorTotal,
                    AgendamentoServicos = agendamentoServicoDTOs,
                    Status = "Agendado"
                };
            }
            catch
            {
                //await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task CriarAsync(AgendamentoInputDTO dto)
        {
            var agendamento = _mapper.Map<Agendamento>(dto);
            await _agendamentoRepository.AddAsync(agendamento);
        }

        public async Task<IEnumerable<AgendamentoOutputDTO>> ListarAsync()
        {
            var lista = await _agendamentoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AgendamentoOutputDTO>>(lista);
        }

        public async Task<AgendamentoOutputDTO> BuscarPorIdAsync(int id)
        {
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);
            return _mapper.Map<AgendamentoOutputDTO>(agendamento);
        }

        public async Task DeletarAsync(int id)
        {
            await _agendamentoRepository.DeleteAsync(id);
        }

    }
}