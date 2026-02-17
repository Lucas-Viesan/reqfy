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

        public SolicitacaoDetalhesDto postSolicitacao(CriarSolicitacaoDto solicitacaoDto)
        {
            Solicitacao solicitacao = _mapper.Map<Solicitacao>(solicitacaoDto);
            solicitacao.Status = "Aberta";
            solicitacao.DataCriacao = DateTimeOffset.Now;
            solicitacao.DataAtualizacao = null;
            _context.Solicitacaos.Add(solicitacao);
            _context.SaveChanges();
            var detalhesDto = _mapper.Map<SolicitacaoDetalhesDto>(solicitacao);
            return detalhesDto;
        }

        public IEnumerable<RecuperaTodasSolicitacoesDto> GetSolicitacaos()
        {
            var solicitacao = _context.Solicitacaos.ToList();
            return _mapper.Map<List<RecuperaTodasSolicitacoesDto>>(solicitacao);
        }

        public RetornaSolicitacaoAtualizadaDto AtualizarSolicitacao(int id, AtualizaSolicitacaoDto atualizaDto)
        {
            var solicitacao = _context.Solicitacaos.FirstOrDefault(s => s.Id == id);
            if (solicitacao == null)
            {
                return null;
            }

            solicitacao.DataAtualizacao = DateTimeOffset.Now;
            solicitacao.Descricao = atualizaDto.Descricao;
            _context.SaveChanges();
            var solicitacaoAtualizada = _mapper.Map<RetornaSolicitacaoAtualizadaDto>(solicitacao);
            return solicitacaoAtualizada;
        }

        public RetornaAtualizacaoStatusDto AtualizarStatus(int id, AtualizaStatusDto atualizaStatusDto)
        {
            var solicitacao = _context.
                Solicitacaos.FirstOrDefault(s => s.Id == id);
            if (solicitacao == null)
            { 
                return null;
            }
            var statusAtual = solicitacao.Status;
            switch (statusAtual)
            {
                case "Aberta":
                    if (atualizaStatusDto.Status != "Em Analise")
                        throw new InvalidOperationException("Status inválido para a situação atual.");
                    solicitacao.Status = "Em Analise";
                    break;

                case "Em Analise":
                    if (atualizaStatusDto.Status == "Aprovada" ||
                        atualizaStatusDto.Status == "Reprovada")
                    {
                        solicitacao.Status = atualizaStatusDto.Status;
                    }
                    else
                    {
                        throw new InvalidOperationException("Status inválido para a situação atual.");
                    }
                    break;

                case "Aprovada":
                case "Reprovada":
                    throw new InvalidOperationException("Solicitação já finalizada.");
            }
            solicitacao.DataAtualizacao = DateTimeOffset.Now;
            _context.SaveChanges();
            var statusAtualizado = _mapper.Map<RetornaAtualizacaoStatusDto>(solicitacao);
            return statusAtualizado;
        }
    }
}
