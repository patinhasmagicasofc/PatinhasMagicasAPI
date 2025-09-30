namespace PatinhasMagicasAPI.Models
{
    using System.Text.Json.Serialization;

    public class TipoServico
    {
        public int IdTipoServico { get; set; }
        public string Nome { get; set; }

        [JsonIgnore] 
        public ICollection<Servico>? Servicos { get; set; }
    }

    public class Servico
        {
            public int IdServico { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public string Status { get; set; }

            // FK
            public int IdTipoServico { get; set; }
            public TipoServico TipoServico { get; set; }
        }
    }

