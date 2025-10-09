using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
