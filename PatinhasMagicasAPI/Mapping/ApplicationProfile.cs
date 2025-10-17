using AutoMapper;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Models.DTOs;

namespace PatinhasMagicasAPI.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {

            CreateMap<Produto, ProdutoOutputDTO>().ReverseMap();
            CreateMap<ProdutoInputDTO, Produto>();
            CreateMap<Endereco, EnderecoOutputDTO>().ReverseMap();
            CreateMap<EnderecoInputDTO, Endereco>();
            CreateMap<Usuario, UsuarioOutputDTO>().ReverseMap();
            CreateMap<UsuarioInputDTO, Usuario>();
            CreateMap<Usuario, LoginUsuarioOutputDTO>()
                .ForMember(dest => dest.Perfil,
                           opt => opt.MapFrom(src => src.TipoUsuario.Nome));
            CreateMap<LoginUsuarioInputDTO, Usuario>();

        }
    }
}
