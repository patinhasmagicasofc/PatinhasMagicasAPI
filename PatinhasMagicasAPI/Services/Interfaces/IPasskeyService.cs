using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IPasskeyService
    {
        Task<PasskeyOptionsResponseDTO> BeginRegistrationAsync(int usuarioId);
        Task<PasskeyOperationResultDTO> CompleteRegistrationAsync(int usuarioId, PasskeyCompleteRequestDTO request);
        Task<PasskeyOptionsResponseDTO> BeginAuthenticationAsync();
        Task<LoginUsuarioOutputDTO> CompleteAuthenticationAsync(PasskeyCompleteRequestDTO request);
        Task<List<PasskeyCredentialOutputDTO>> GetByUsuarioAsync(int usuarioId);
        Task RemoveAsync(int usuarioId, int credentialId);
    }
}
