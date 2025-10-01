using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PatinhasMagicasAPI.Models
{
    public class TipoServico
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }


        [JsonIgnore]
        public ICollection<Servico>? Servicos { get; set; }
    }
}

