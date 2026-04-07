using AutoMapper;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PatinhasMagicasAPI.Services
{
    public class PasskeyService : IPasskeyService
    {
        private const string RegistrationCachePrefix = "passkeys.registration.";
        private const string AuthenticationCachePrefix = "passkeys.authentication.";

        private readonly IFido2 _fido2;
        private readonly IMemoryCache _memoryCache;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasskeyCredentialRepository _passkeyCredentialRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly PasskeySettings _settings;

        public PasskeyService(
            IFido2 fido2,
            IMemoryCache memoryCache,
            IUsuarioRepository usuarioRepository,
            IPasskeyCredentialRepository passkeyCredentialRepository,
            ITokenService tokenService,
            IMapper mapper,
            IOptions<PasskeySettings> settings)
        {
            _fido2 = fido2;
            _memoryCache = memoryCache;
            _usuarioRepository = usuarioRepository;
            _passkeyCredentialRepository = passkeyCredentialRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<PasskeyOptionsResponseDTO> BeginRegistrationAsync(int usuarioId)
        {
            EnsureConfigured();

            var usuario = await GetUsuarioValidoAsync(usuarioId);
            var existingCredentials = await _passkeyCredentialRepository.GetByUsuarioIdAsync(usuarioId);

            var fidoUser = new Fido2User
            {
                DisplayName = usuario.Nome,
                Name = usuario.Email,
                Id = Encoding.UTF8.GetBytes(usuario.Id.ToString())
            };

            var excludeCredentials = existingCredentials
                .Select(credential => new PublicKeyCredentialDescriptor(Base64UrlDecode(credential.CredentialId)))
                .ToList();

            var authenticatorSelection = new AuthenticatorSelection
            {
                ResidentKey = ResidentKeyRequirement.Required,
                UserVerification = UserVerificationRequirement.Required
            };

            var options = _fido2.RequestNewCredential(new RequestNewCredentialParams
            {
                User = fidoUser,
                ExcludeCredentials = excludeCredentials,
                AuthenticatorSelection = authenticatorSelection,
                AttestationPreference = AttestationConveyancePreference.None
            });

            var flowId = Guid.NewGuid().ToString("N");

            _memoryCache.Set(
                RegistrationCachePrefix + flowId,
                new RegistrationState(usuarioId, options),
                TimeSpan.FromMinutes(5));

            return new PasskeyOptionsResponseDTO
            {
                FlowId = flowId,
                PublicKey = options
            };
        }

        public async Task<PasskeyOperationResultDTO> CompleteRegistrationAsync(int usuarioId, PasskeyCompleteRequestDTO request)
        {
            EnsureConfigured();

            var state = GetRegistrationState(request.FlowId);

            if (state.UsuarioId != usuarioId)
            {
                throw new UnauthorizedAccessException("Fluxo de biometria invalido para este usuario.");
            }

            var attestationResponse = JsonSerializer.Deserialize<AuthenticatorAttestationRawResponse>(request.Credential.GetRawText())
                ?? throw new ArgumentException("Resposta da biometria invalida.");

            var credentialResult = await _fido2.MakeNewCredentialAsync(new MakeNewCredentialParams
            {
                AttestationResponse = attestationResponse,
                OriginalOptions = state.Options,
                IsCredentialIdUniqueToUserCallback = IsCredentialIdUniqueToUserAsync
            });

            var credential = new PasskeyCredential
            {
                UsuarioId = usuarioId,
                CredentialId = Base64UrlEncode(credentialResult.Id),
                UserHandle = Base64UrlEncode(state.Options.User.Id),
                PublicKey = Base64UrlEncode(credentialResult.PublicKey),
                SignatureCounter = credentialResult.SignCount,
                CredType = credentialResult.Type.ToString(),
                AaGuid = credentialResult.AaGuid.ToString(),
                FriendlyName = BuildFriendlyName(),
                Transports = credentialResult.Transports is null ? TryGetTransports(request.Credential) : string.Join(",", credentialResult.Transports),
                CreatedAt = DateTime.UtcNow
            };

            await _passkeyCredentialRepository.AddAsync(credential);
            _memoryCache.Remove(RegistrationCachePrefix + request.FlowId);

            return new PasskeyOperationResultDTO
            {
                Success = true,
                Message = "Biometria cadastrada com sucesso neste dispositivo."
            };
        }

        public Task<PasskeyOptionsResponseDTO> BeginAuthenticationAsync()
        {
            EnsureConfigured();

            var options = _fido2.GetAssertionOptions(new GetAssertionOptionsParams
            {
                AllowedCredentials = Array.Empty<PublicKeyCredentialDescriptor>(),
                UserVerification = UserVerificationRequirement.Required
            });

            var flowId = Guid.NewGuid().ToString("N");

            _memoryCache.Set(
                AuthenticationCachePrefix + flowId,
                new AuthenticationState(options),
                TimeSpan.FromMinutes(5));

            return Task.FromResult(new PasskeyOptionsResponseDTO
            {
                FlowId = flowId,
                PublicKey = options
            });
        }

        public async Task<LoginUsuarioOutputDTO> CompleteAuthenticationAsync(PasskeyCompleteRequestDTO request)
        {
            EnsureConfigured();

            var state = GetAuthenticationState(request.FlowId);
            var credentialId = request.Credential.GetProperty("id").GetString();

            if (string.IsNullOrWhiteSpace(credentialId))
            {
                throw new ArgumentException("Identificador da credencial nao foi informado.");
            }

            var storedCredential = await _passkeyCredentialRepository.GetByCredentialIdAsync(credentialId)
                ?? throw new UnauthorizedAccessException("Credencial biometrica nao encontrada.");

            var assertionResponse = JsonSerializer.Deserialize<AuthenticatorAssertionRawResponse>(request.Credential.GetRawText())
                ?? throw new ArgumentException("Resposta da biometria invalida.");

            var result = await _fido2.MakeAssertionAsync(new MakeAssertionParams
            {
                AssertionResponse = assertionResponse,
                OriginalOptions = state.Options,
                StoredPublicKey = Base64UrlDecode(storedCredential.PublicKey),
                StoredSignatureCounter = storedCredential.SignatureCounter,
                IsUserHandleOwnerOfCredentialIdCallback = (args, _) => Task.FromResult(
                    Base64UrlEncode(args.CredentialId) == storedCredential.CredentialId &&
                    Base64UrlEncode(args.UserHandle) == storedCredential.UserHandle)
            });

            storedCredential.SignatureCounter = result.SignCount;
            storedCredential.LastUsedAt = DateTime.UtcNow;
            await _passkeyCredentialRepository.UpdateAsync(storedCredential);
            _memoryCache.Remove(AuthenticationCachePrefix + request.FlowId);

            var usuario = await GetUsuarioValidoAsync(storedCredential.UsuarioId);
            var token = _tokenService.GenerateToken(usuario);
            var loginUsuarioOutputDTO = _mapper.Map<LoginUsuarioOutputDTO>(usuario);
            loginUsuarioOutputDTO.Token = token;

            return loginUsuarioOutputDTO;
        }

        public async Task<List<PasskeyCredentialOutputDTO>> GetByUsuarioAsync(int usuarioId)
        {
            var credentials = await _passkeyCredentialRepository.GetByUsuarioIdAsync(usuarioId);

            return credentials.Select(credential => new PasskeyCredentialOutputDTO
            {
                Id = credential.Id,
                FriendlyName = credential.FriendlyName,
                CreatedAt = credential.CreatedAt,
                LastUsedAt = credential.LastUsedAt
            }).ToList();
        }

        public async Task RemoveAsync(int usuarioId, int credentialId)
        {
            var credential = await _passkeyCredentialRepository.GetByIdAsync(credentialId)
                ?? throw new KeyNotFoundException("Credencial biometrica nao encontrada.");

            if (credential.UsuarioId != usuarioId)
            {
                throw new UnauthorizedAccessException("Esta credencial nao pertence ao usuario autenticado.");
            }

            await _passkeyCredentialRepository.DeleteAsync(credential);
        }

        private async Task<Usuario> GetUsuarioValidoAsync(int usuarioId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario nao encontrado.");
            }

            if (!usuario.Ativo)
            {
                throw new UnauthorizedAccessException("Usuario inativo. Entre em contato com o suporte.");
            }

            return usuario;
        }

        private async Task<bool> IsCredentialIdUniqueToUserAsync(IsCredentialIdUniqueToUserParams args, CancellationToken cancellationToken)
        {
            var credentialId = Base64UrlEncode(args.CredentialId);
            var existingCredential = await _passkeyCredentialRepository.GetByCredentialIdAsync(credentialId);
            return existingCredential == null;
        }

        private RegistrationState GetRegistrationState(string flowId)
        {
            if (!_memoryCache.TryGetValue(RegistrationCachePrefix + flowId, out RegistrationState? state) || state == null)
            {
                throw new InvalidOperationException("O cadastro da biometria expirou. Tente novamente.");
            }

            return state;
        }

        private AuthenticationState GetAuthenticationState(string flowId)
        {
            if (!_memoryCache.TryGetValue(AuthenticationCachePrefix + flowId, out AuthenticationState? state) || state == null)
            {
                throw new InvalidOperationException("O login com biometria expirou. Tente novamente.");
            }

            return state;
        }

        private void EnsureConfigured()
        {
            if (string.IsNullOrWhiteSpace(_settings.RpId) || _settings.Origins.Count == 0)
            {
                throw new InvalidOperationException("Passkeys nao configuradas. Ajuste a secao Passkeys no appsettings da API.");
            }
        }

        private static string BuildFriendlyName()
        {
            return $"Dispositivo {DateTime.Now:dd/MM/yyyy HH:mm}";
        }

        private static string? TryGetTransports(JsonElement credential)
        {
            if (!credential.TryGetProperty("response", out var response) ||
                !response.TryGetProperty("transports", out var transports) ||
                transports.ValueKind != JsonValueKind.Array)
            {
                return null;
            }

            var values = transports.EnumerateArray()
                .Select(item => item.GetString())
                .Where(value => !string.IsNullOrWhiteSpace(value));

            return string.Join(",", values!);
        }

        private static string Base64UrlEncode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private static byte[] Base64UrlDecode(string value)
        {
            var normalized = value.Replace('-', '+').Replace('_', '/');

            switch (normalized.Length % 4)
            {
                case 2:
                    normalized += "==";
                    break;
                case 3:
                    normalized += "=";
                    break;
            }

            return Convert.FromBase64String(normalized);
        }

        private sealed record RegistrationState(int UsuarioId, CredentialCreateOptions Options);
        private sealed record AuthenticationState(AssertionOptions Options);
    }
}
