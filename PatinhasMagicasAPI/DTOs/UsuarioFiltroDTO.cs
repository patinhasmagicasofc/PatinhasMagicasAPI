namespace PatinhasMagicasAPI.DTOs
{
    public class UsuarioFiltroDTO
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Nome { get; set; }
        public string? TipoUsuario { get; set; }

        // Opcional: paginação
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
