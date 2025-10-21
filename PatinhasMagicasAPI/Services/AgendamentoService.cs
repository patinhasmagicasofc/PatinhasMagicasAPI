﻿using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Repositories;
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

            var animal = await _animalRepository.GetByIdAsync(agendamentoCreateDTO.AnimalId);
            if (animal == null) throw new Exception("Animal não encontrado.");

            var servicoTamanho = await _servicoTamanhoRepository
                .GetByServicoAndTamanhoAsync(agendamentoCreateDTO.ServicoId, animal.TamanhoAnimalId);
            if (servicoTamanho == null)
                throw new Exception($"Serviço {agendamentoCreateDTO.ServicoId} não disponível para este animal.");

            // Criar Pedido
            var pedido = await _pedidoRepository.Add(new Pedido
            {
                UsuarioId = agendamentoCreateDTO.UsuarioId ?? 0,
                StatusPedidoId = 1,
                DataPedido = DateTime.Now
            });

            // Criar Agendamento
            var agendamento = await _agendamentoRepository.Add(new Agendamento
            {
                AnimalId = agendamentoCreateDTO.AnimalId,
                PedidoId = pedido.Id,
                StatusAgendamentoId = 1,
                DataAgendamento = agendamentoCreateDTO.DataAgendamento ?? DateTime.Now,
                DataCadastro = DateTime.Now
            });

            // Criar AgendamentoServico
            var agendamentoServico = new AgendamentoServico
            {
                AgendamentoId = agendamento.Id,
                ServicoId = agendamentoCreateDTO.ServicoId,
                Preco = servicoTamanho.Preco
            };

            await _agendamentoServicoRepository.AddAsync(agendamentoServico);


            // Retorno DTO
            return new AgendamentoOutputDTO
            {
                Id = agendamento.Id,
                PedidoId = pedido.Id,
                DataAgendamento = agendamento.DataAgendamento,
                ValorTotal = servicoTamanho.Preco,
                AgendamentoServicos = new List<AgendamentoServicoDTO>
            {
                new AgendamentoServicoDTO
                {
                    Id = agendamentoCreateDTO.ServicoId,
                    Preco = servicoTamanho.Preco
                }
            },
                Status = "Agendado"
            };
        }

        public async Task<AgendamentoDetalhesDTO> GetByIdAsync(int id)
        {
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);
            if (agendamento == null) return null;

            var pedido = agendamento.Pedido;

            var pagamento = pedido?.Pagamentos?
                .Where(p => p.StatusPagamento != null && p.StatusPagamento.Nome == "Pago")
                .OrderByDescending(p => p.DataPagamento)
                .FirstOrDefault()
                ?? pedido?.Pagamentos?.OrderByDescending(p => p.DataPagamento).FirstOrDefault();

            var tipoPagamento = pagamento?.TipoPagamento?.Nome ?? "Não informado";
            var dataConfirmacao = pagamento?.DataPagamento ?? pedido?.DataPedido;

            return new AgendamentoDetalhesDTO
            {
                Id = agendamento.Id,
                DataAgendamento = agendamento.DataAgendamento,
                DataConfirmacao = dataConfirmacao ?? default(DateTime),
                Status = agendamento.StatusAgendamento?.Nome,
                TipoPagamento = tipoPagamento,
                ValorTotal = agendamento.AgendamentoServicos.Sum(s => s.Preco),
                Animal = new AnimalOutputDTO { Id = agendamento.Animal.Id, Nome = agendamento.Animal.Nome, Especie = agendamento.Animal.Especie, NomeTamanhoAnimal = agendamento.Animal.TamanhoAnimal.Nome },
                Servicos = agendamento.AgendamentoServicos.Select(s => new ServicoOutputDTO { Id = s.Servico.Id, Nome = s.Servico.Nome, TipoServicoNome = s.Servico.TipoServico.Nome, Preco = s.Preco }).ToList()
            };
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