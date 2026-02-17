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
            CreateMap<Solicitacao, SolicitacaoDetalhesDto>();
            CreateMap<Solicitacao, RecuperaTodasSolicitacoesDto>();
            CreateMap<Solicitacao, RetornaSolicitacaoPorIdDto>();
            CreateMap<Solicitacao, RetornaSolicitacaoAtualizadaDto>();
            CreateMap<Solicitacao, RetornaAtualizacaoStatusDto>();

        }

    }
}
