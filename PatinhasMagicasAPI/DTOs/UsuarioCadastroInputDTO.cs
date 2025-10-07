using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.DTOs
{
    public class UsuarioCadastroInputDTO
    {

        // Propriedades mínimas para cadastro
        public string Nome { get; set; }

        public string Email { get; set; }

        // A senha deve ser obrigatória e forte
        public string Senha { get; set; }

        // Opcional: Se precisar de um campo de confirmação de senha
        [Compare("Senha", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
        public string ConfirmaSenha { get; set; }

        // Opcional: Tipo de usuário padrão (Cliente)
        public int TipoUsuarioId { get; set; } = 2; 

    }
}
