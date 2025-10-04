﻿namespace PatinhasMagicasAPI.DTOs
{
    public class EnderecoOutputDTO
    {
        public int IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string? Complemento { get; set; }
        public int UsuarioId { get; set; }
    }
}