namespace PatinhasMagicasAPI.DTOs
{
    public class DashboardUsuarioDTO
    {
        public int TotalUsuarios { get; set; }
        public int TotalUsuariosAtivos { get; set; }
        public int TotalUsuariosInativos { get; set; }
        public int TotalUsuariosCliente { get; set; }
        public List<UsuarioOutputDTO> UsuarioOutputDTOs { get; set; }
    }
}