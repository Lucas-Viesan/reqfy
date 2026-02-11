using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReqFy.Data;
using ReqFy.Dtos;
using ReqFy.Models;

namespace ReqFy.Service
{
    public class SolicitacaoService
    {
        private SolicitacaoContext _context;
        private IMapper _mapper;
        public SolicitacaoService(SolicitacaoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Solicitacao postSolicitacao(CriarSolicitacaoDto solicitacaoDto)
        {
            Solicitacao solicitacao = _mapper.Map<Solicitacao>(solicitacaoDto);
            solicitacao.Status = "Aberta";
            solicitacao.DataCriacao = DateTimeOffset.Now;
            solicitacao.DataAtualizacao = null;
            _context.Solicitacaos.Add(solicitacao);
            _context.SaveChanges();
            return solicitacao;
        }
    }

}
