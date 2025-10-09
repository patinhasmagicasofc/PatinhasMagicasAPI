using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Models.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IMapper _mapper;
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IMapper mapper, IEnderecoRepository enderecoRepository)
        {
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<List<EnderecoOutputDTO>> GetAsync()
        {
            var enderecos = await _enderecoRepository.GetAsync();
            return _mapper.Map<List<EnderecoOutputDTO>>(enderecos);
        }

        public async Task<EnderecoOutputDTO> GetByIdAsync(int id)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);
            return _mapper.Map<EnderecoOutputDTO>(endereco);
        }

        public async Task AddAsync(EnderecoInputDTO enderecoInputDTO)
        {
            var endereco = _mapper.Map<Endereco>(enderecoInputDTO);
            await _enderecoRepository.AddAsync(endereco);
        }

        public async Task UpdateAsync(int id, EnderecoInputDTO enderecoInputDTO)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);
            if (endereco != null)
            {
                await _enderecoRepository.UpdateAsync(endereco);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var endereco = await _enderecoRepository.GetByIdAsync(id);
            if (endereco != null)
            {
               await _enderecoRepository.DeleteAsync(endereco);
            }
        }
    }
}
