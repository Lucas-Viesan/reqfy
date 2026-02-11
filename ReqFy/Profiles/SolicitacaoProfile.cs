using AutoMapper;
using ReqFy.Dtos;
using ReqFy.Models;

namespace ReqFy.Profiles
{
    public class SolicitacaoProfile : Profile
    {
        public SolicitacaoProfile()
        {
            CreateMap<CriarSolicitacaoDto, Solicitacao>();
        }

    }
}
