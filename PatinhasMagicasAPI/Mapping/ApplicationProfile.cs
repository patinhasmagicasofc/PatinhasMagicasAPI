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

            CreateMap<Pedido, PedidoOutputDTO>()
                .ForMember(dest => dest.NomeUsuario,
                           opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(dest => dest.UsuarioOutputDTO,
                           opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.ItemPedidoOutputDTOs,
                           opt => opt.MapFrom(src => src.ItensPedido));

            CreateMap<Categoria, CategoriaOuputDTO>().ReverseMap();
            CreateMap<CategoriaInputDTO, Categoria>();

            CreateMap<Agendamento, AgendamentoOutputDTO>().ReverseMap();
            CreateMap<AgendamentoInputDTO, Agendamento>();

            CreateMap<Pagamento, PagamentoOutputDTO>().ReverseMap();
            CreateMap<PagamentoInputDTO, Pagamento>();

            CreateMap<Pedido, PedidoOutputDTO>().ReverseMap();
            CreateMap<PedidoInputDTO, Pedido>();

            CreateMap<ServicoTamanho, ServicoOutputDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Servico.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Servico.Nome))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Servico.Descricao))
                .ForMember(dest => dest.TempoEstimadoMinutos, opt => opt.MapFrom(src => src.Servico.TempoEstimadoMinutos))
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco));

            CreateMap<Animal, AnimalInputDTO>().ReverseMap();
            CreateMap<AnimalInputDTO, Animal>();

            CreateMap<TamanhoAnimal, TamanhoAnimalOutputDTO>().ReverseMap();
            CreateMap<TamanhoAnimalInputDTO, TamanhoAnimal>();
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
