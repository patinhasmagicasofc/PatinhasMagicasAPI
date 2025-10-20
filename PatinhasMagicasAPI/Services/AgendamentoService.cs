using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Repositories;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IAgendamentoRepository _agendamentoRepo;
        private readonly IServicoTamanhoRepository _servicoTamanhoRepo;
        private readonly IMapper _mapper;

        public AgendamentoService(
            IPedidoRepository pedidoRepo,
            IAgendamentoRepository agendamentoRepo,
            IServicoTamanhoRepository servicoTamanhoRepo,
            IMapper mapper)
        {
            _pedidoRepo = pedidoRepo;
            _agendamentoRepo = agendamentoRepo;
            _servicoTamanhoRepo = servicoTamanhoRepo;
            _mapper = mapper;
        }

        public async Task<AgendamentoResponseDTO> CriarAgendamentoAsync(AgendamentoCreateDTO dto)
        {
            using var transaction = await (_agendamentoRepo as AgendamentoRepository)!._context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Buscar o tamanho do animal
                var animal = await (_agendamentoRepo as AgendamentoRepository)!._context.Animal.FindAsync(dto.IdAnimal);
                if (animal == null) throw new Exception("Animal não encontrado");

                decimal valorTotal = 0;
                var servicosDetalhes = new List<AgendamentoServicoDTO>();

                foreach (var idServico in dto.IdServicos)
                {
                    var st = await _servicoTamanhoRepo.GetByServicoAndTamanhoAsync(idServico, animal.IdTamanhoAnimal);
                    if (st == null) throw new Exception($"Serviço {idServico} não disponível para este animal");

                    valorTotal += st.preco;
                    servicosDetalhes.Add(new AgendamentoServicoDTO { IdServico = idServico, Preco = st.preco });
                }

                // 2️⃣ Criar Pedido
                var pedido = await _pedidoRepo.AddAsync(new Pedido
                {
                    IdUsuario = dto.IdUsuario,
                    IdStatusPedido = 1,
                    dataPedido = DateTime.Now
                });

                // 3️⃣ Criar Agendamento
                var agendamento = await _agendamentoRepo.AddAsync(new Agendamento
                {
                    IdAnimal = dto.IdAnimal,
                    IdPedido = pedido.IdPedido,
                    IdStatusAgendamento = 1,
                    dataAgendamento = dto.DataAgendamento,
                    dataCadastro = DateTime.Now
                });

                // 4️⃣ Criar AgendamentoServico
                var agendamentoServicos = servicosDetalhes.Select(s => new AgendamentoServico
                {
                    IdAgendamento = agendamento.IdAgendamento,
                    IdServico = s.IdServico,
                    preco = s.Preco
                }).ToList();

                await _agendamentoRepo.AddAgendamentoServicosAsync(agendamentoServicos);

                await transaction.CommitAsync();

                return new AgendamentoResponseDTO
                {
                    IdAgendamento = agendamento.IdAgendamento,
                    IdPedido = pedido.IdPedido,
                    DataAgendamento = agendamento.dataAgendamento,
                    ValorTotal = valorTotal,
                    Servicos = servicosDetalhes,
                    Status = "Agendado"
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task CriarAsync(AgendamentoInputDTO dto)
        {
            var agendamento = _mapper.Map<Agendamento>(dto);
            await _repo.AddAsync(agendamento);
        }

        public async Task<IEnumerable<AgendamentoOutputDTO>> ListarAsync()
        {
            var lista = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<AgendamentoOutputDTO>>(lista);
        }

        public async Task<AgendamentoOutputDTO> BuscarPorIdAsync(int id)
        {
            var agendamento = await _repo.GetByIdAsync(id);
            return _mapper.Map<AgendamentoOutputDTO>(agendamento);
        }

        public async Task DeletarAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

    }
}