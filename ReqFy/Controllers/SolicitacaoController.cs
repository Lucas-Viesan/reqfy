using AutoMapper;
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
        private IMapper _mapper;
        public SolicitacaoController(SolicitacaoService service, SolicitacaoContext context, IMapper mapper)
        {
            _service = service;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CriarSolicitacao([FromBody] CriarSolicitacaoDto solicitacaoDto)
        {
            var dto = _service.postSolicitacao(solicitacaoDto);

            return CreatedAtAction(nameof(RetornarSolicitacaoPorId), new { id = dto.Id }, dto);
        }

        [HttpGet]
        public IActionResult RetornarTodasSolicitacoes()
        {
            var dto = _service.GetSolicitacaos();

            return Ok(dto);

        }

        [HttpGet("{id}")]
        public IActionResult RetornarSolicitacaoPorId(int id)
        {
            var dto = _service.GetSolicitacaoById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarSolicitacao(int id, [FromBody] AtualizaSolicitacaoDto atualizaDto)
        {
            var solicitacaoAtualizada = _service.AtualizarSolicitacao(id, atualizaDto);

            if (solicitacaoAtualizada == null)
            {
                return NotFound();
            }
            return Ok(solicitacaoAtualizada);
        }

        [HttpPut("{id}/status")]
        public IActionResult AtualizarStatus(int id, [FromBody] AtualizaStatusDto atualizaStatusDto)
        {
            if (atualizaStatusDto == null)
                return BadRequest("Dados inválidos.");

            try
            {
                var statusAtualizado = _service.AtualizarStatus(id, atualizaStatusDto);

                if (statusAtualizado == null)
                    return NotFound();

                return Ok(statusAtualizado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
