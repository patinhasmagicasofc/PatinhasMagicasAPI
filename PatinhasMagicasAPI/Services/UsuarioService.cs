using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task AddAsync(UsuarioInputDTO usuarioInputDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioInputDTO);
            await _usuarioRepository.AddAsync(usuario);
        }

        public Task InativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsuarioOutputDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<List<UsuarioOutputDTO>>(usuarios);
        }

        public async Task<UsuarioOutputDTO> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioOutputDTO>(usuario);
        }

        public async Task UpdateAsync(int id, UsuarioInputDTO usuarioInputDTO)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario != null)
            {
                _mapper.Map(usuarioInputDTO, usuario);
                await _usuarioRepository.UpdateAsync(usuario);
            }
        }

        public async Task<LoginUsuarioOutputDTO>? ValidarLoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.ValidarLoginAsync(email, senha);
            return _mapper.Map<LoginUsuarioOutputDTO>(usuario);
        }

    }
}
