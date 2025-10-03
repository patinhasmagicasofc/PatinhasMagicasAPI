using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class UsuarioOutputDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public int Ddd { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}
