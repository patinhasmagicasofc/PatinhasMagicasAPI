using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;

        public ServicoService(IServicoRepository servicoRepository, IMapper mapper, IAnimalRepository animalRepository)
        {
            _servicoRepository = servicoRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
        }

        public async Task<IEnumerable<ServicoOutputDTO>> GetServicosPorAnimalAsync(int idAnimal)
        {
            var animal = await _animalRepository.GetByIdAsync(idAnimal);
            if (animal == null)
                return Enumerable.Empty<ServicoOutputDTO>();

            var servicoTamanhos = await _servicoRepository.GetServicosPorTamanhoAsync(animal.TamanhoAnimalId);

            return _mapper.Map<IEnumerable<ServicoOutputDTO>>(servicoTamanhos);
        }
    }
}