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
        private readonly ITokenService _tokenService;
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, ITokenService tokenService, ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        public async Task AddAsync(UsuarioInputDTO usuarioInputDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioInputDTO);

            if (!await ValidateEmailAsync(usuario.Email))
                throw new ArgumentException("Email já cadastrado.");

            if (!await ValidateCPFAsync(usuario.CPF))
                throw new ArgumentException("CPF já cadastrado.");

            usuario.TipoUsuarioId = await GetTipoUsuarioIdAsync(usuario.TipoUsuarioId);

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            await _usuarioRepository.AddAsync(usuario);
        }

        private async Task<bool> ValidateEmailAsync(string email)
        {
            var existingEmail = await _usuarioRepository.GetByEmailAsync(email);
            return existingEmail == null;
        }

        private async Task<bool> ValidateCPFAsync(string cpf)
        {
            var existingCPF = await _usuarioRepository.GetByCPFAsync(cpf);
            return existingCPF == null;
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

        public async Task UpdateAsync(int id, UsuarioUpdateDTO usuarioUpdateDTO)
        {
            //var usuario = _mapper.Map<Usuario>(usuarioUpdateDTO);

            var usuario = new Usuario
            {
                Id = id,
                Nome = usuarioUpdateDTO.Nome,
                Ddd = usuarioUpdateDTO.Ddd.Value,
                Email = usuarioUpdateDTO.Email,
                Telefone = usuarioUpdateDTO.Telefone,
                TipoUsuarioId = usuarioUpdateDTO.TipoUsuarioId.Value
            };

            usuario.Id = id;

            var usuarioExiste = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExiste == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            if (!await ValidateEmailAsync(usuario.Email, id))
                throw new ArgumentException("Email já cadastrado por outro usuário.");

            //if (!await ValidateCPFAsync(usuario.CPF, id))
            //    throw new ArgumentException("CPF já cadastrado por outro usuário.");

            //if (!string.IsNullOrEmpty(usuario.Senha))
            //    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            usuario.CPF = usuarioExiste.CPF;
            usuario.Senha = usuarioExiste.Senha;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        private async Task<bool> ValidateEmailAsync(string email, int userId)
        {
            var existingUser = await _usuarioRepository.GetByEmailAsync(email);
            return existingUser == null || existingUser.Id == userId;
        }

        private async Task<bool> ValidateCPFAsync(string cpf, int userId)
        {
            var existingUser = await _usuarioRepository.GetByCPFAsync(cpf);
            return existingUser == null || existingUser.Id == userId;
        }

        public async Task<LoginUsuarioOutputDTO>? ValidarLoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);

            if (usuario == null)
                throw new ArgumentException("Usuário ou senha inválidos!");

            bool isValid = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

            if (!isValid)
                throw new ArgumentException("Usuário ou senha inválidos!");

            string role = usuario.TipoUsuario?.DescricaoTipoUsuario ?? "Cliente";

            var token = _tokenService.GenerateToken(usuario);

            var loginUsuarioOutputDTO = _mapper.Map<LoginUsuarioOutputDTO>(usuario);
            loginUsuarioOutputDTO.Token = token;
            return loginUsuarioOutputDTO;
        }

        public async Task<int> GetTipoUsuarioIdAsync(int? idTipoUsuario)
        {
            if (idTipoUsuario != null && idTipoUsuario > 0)
                return idTipoUsuario.Value;

            var tipoUsuario = await _tipoUsuarioRepository.GetByNomeAsync("Cliente");

            if (tipoUsuario == null)
            {
                tipoUsuario = new TipoUsuario
                {
                    DescricaoTipoUsuario = "Cliente"
                };

                await _tipoUsuarioRepository.AddAsync(tipoUsuario);
            }

            return tipoUsuario.IdTipoUsuario;
        }

        public Task InativarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReativarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
