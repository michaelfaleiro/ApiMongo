using ApiMongo.Dtos;
using ApiMongo.Models;
using AutoMapper;

namespace ApiMongo.Profiles
{
    public class OrcamentoProfile : Profile
    {
        public OrcamentoProfile()
        {
            CreateMap<CreateOrcamentoDto, Orcamento>();
            CreateMap<AddProdutoOrcamentoDto, Produto>();
        }
    }
}