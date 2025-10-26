namespace PatinhasMagicasAPI.DTOs
{
    public class AnimalInputDTO
    {
        public string Nome { get; set; }
        public int EspecieId { get; set; }
        public string Raca { get; set; }
        public int? Idade { get; set; }
        public int TamanhoAnimalId { get; set; }
        public int UsuarioId { get; set; }
    }
}
