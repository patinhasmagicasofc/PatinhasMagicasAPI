namespace PatinhasMagicasAPI.DTOs
{
    public class UsuarioOutputDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Ddd { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string TipoUsuarioNome { get; set; }
        public EnderecoOutputDTO Endereco { get; set; }
    }
}
