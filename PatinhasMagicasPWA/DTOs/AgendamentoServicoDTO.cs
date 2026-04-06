using System.ComponentModel.DataAnnotations;

namespace PatinhasMagicasPWA.DTOs
{
    public class AgendamentoServicoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do animal.")]
        public string Nome { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um animal.")]
        public int? AnimalId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um serviço.")]
        public int? ServicoId { get; set; }

        public string NomeEspecie { get; set; }

        [Required(ErrorMessage = "Informe a raça.")]
        public string Raca { get; set; }

        [Required(ErrorMessage = "Informe a idade.")]
        public int? Idade { get; set; }

        public string? FotoDataUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tamanho.")]
        public int? TamanhoAnimalId { get; set; }

        public string NomeTamanhoAnimal { get; set; }

        public int UsuarioId { get; set; }

        public string NomeUsuario { get; set; }
    }
}
