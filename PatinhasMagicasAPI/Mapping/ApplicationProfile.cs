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
            CreateMap<Endereco, EnderecoOutputDTO>().ReverseMap();
            CreateMap<EnderecoInputDTO, Endereco>();
        }
    }
}
