using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ReqFy.Data;
using ReqFy.Dtos;
using ReqFy.Models;
using ReqFy.Service;
using System.Diagnostics.Eventing.Reader;

namespace ReqFy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolicitacaoController : ControllerBase
    {
        private SolicitacaoService _service;
        private SolicitacaoContext _context;
        public SolicitacaoController(SolicitacaoService service, SolicitacaoContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost]
        public IActionResult CriarSolicitacao([FromBody] CriarSolicitacaoDto solicitacaoDto)
        {
             Solicitacao solicitacao = _service.postSolicitacao(solicitacaoDto);

            return Created("", solicitacao);
        }

        [HttpGet]
        public IEnumerable<Solicitacao> GetSolicitacaos()
        {
            return _context.Solicitacaos.ToList();
        }

    }
}
