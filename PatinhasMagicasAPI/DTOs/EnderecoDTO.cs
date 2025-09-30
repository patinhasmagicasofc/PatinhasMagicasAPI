using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasAPI.Models.DTOs
{
    public class EnderecoDTO
    {
        public int IdEndereco { get; set; }

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(200, ErrorMessage = "O logradouro deve ter no máximo 200 caracteres.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        [Range(1, 9999999, ErrorMessage = "O número deve estar entre 1 e 9999999.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter 2 caracteres (UF).")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve ter 9 caracteres (99999-999).")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido. Use 99999-999.")]
        public string CEP { get; set; }

        [StringLength(200, ErrorMessage = "O complemento deve ter no máximo 200 caracteres.")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }
    }
}