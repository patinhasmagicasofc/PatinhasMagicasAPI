using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class TamanhoAnimalService : ITamanhoAnimalService
    {
        private readonly ITamanhoAnimalRepository _repository;
        private readonly IMapper _mapper;

        public TamanhoAnimalService(ITamanhoAnimalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TamanhoAnimalOutputDTO> CreateAsync(TamanhoAnimalInputDTO dto)
        {
            var entity = _mapper.Map<TamanhoAnimal>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<TamanhoAnimalOutputDTO>(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TamanhoAnimalOutputDTO>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TamanhoAnimalOutputDTO>>(list);
        }

        public async Task<TamanhoAnimalOutputDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TamanhoAnimalOutputDTO>(entity);
        }

        public async Task<TamanhoAnimalOutputDTO> UpdateAsync(int id, TamanhoAnimalInputDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Nome = dto.Nome;
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<TamanhoAnimalOutputDTO>(updated);
        }
    }
}