namespace PatinhasMagicasAPI.DTOs
{
    public class AnimalOutputDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EspecieId { get; set; }
        public string NomeEspecie { get; set; }
        public string Raca { get; set; }
        public int? Idade { get; set; }
        public int TamanhoAnimalId { get; set; }
        public string NomeTamanhoAnimal { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
    }
}
