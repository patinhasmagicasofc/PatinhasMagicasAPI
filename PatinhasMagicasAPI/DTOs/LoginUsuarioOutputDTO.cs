using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.DTOs
{
    public class LoginUsuarioOutputDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
