using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repository;
        private readonly IMapper _mapper;

        public AnimalService(IAnimalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AnimalOutputDTO> CreateAsync(AnimalInputDTO dto)
        {
            var entity = _mapper.Map<Animal>(dto);
            var created = await _repository.AddAsync(entity);
            return await MapToOutputDTO(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AnimalOutputDTO>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return await Task.WhenAll(list.Select(a => MapToOutputDTO(a)));
        }

        public async Task<IEnumerable<AnimalOutputDTO>> GetAllByUsuarioId(int usuarioId)
        {
            var list = await _repository.GetAnimalsByUsuarioIdAsync(usuarioId);
            return await Task.WhenAll(list.Select(a => MapToOutputDTO(a)));
        }

        public async Task<AnimalOutputDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return await MapToOutputDTO(entity);
        }

        public async Task<AnimalOutputDTO> UpdateAsync(int id, AnimalInputDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Nome = dto.Nome;
            entity.Especie = dto.Especie;
            entity.Raca = dto.Raca;
            entity.Idade = dto.Idade;
            entity.TamanhoAnimalId = dto.TamanhoAnimalId;
            entity.UsuarioId = dto.UsuarioId;

            var updated = await _repository.UpdateAsync(entity);
            return await MapToOutputDTO(updated);
        }

        private async Task<AnimalOutputDTO> MapToOutputDTO(Animal animal)
        {
            return new AnimalOutputDTO
            {
                Id = animal.Id,
                Nome = animal.Nome,
                Especie = animal.Especie,
                Raca = animal.Raca,
                Idade = animal.Idade,
                TamanhoAnimalId = animal.TamanhoAnimalId,
                NomeTamanhoAnimal = animal.TamanhoAnimal?.Nome,
                UsuarioId = animal.UsuarioId,
                NomeUsuario = animal.Usuario?.Nome
            };
        }
    }
}