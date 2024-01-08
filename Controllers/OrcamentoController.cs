using ApiMongo.Dtos;
using ApiMongo.Extensions;
using ApiMongo.Models;
using ApiMongo.Services;
using ApiMongo.ViewsModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiMongo.Controllers
{
    [ApiController]
    [Route("api/")]
    public class OrcamentoController : ControllerBase
    {
        private readonly OrcamentoService _orcamentoService;
        private readonly IMapper _mapper;
        public OrcamentoController(OrcamentoService orcamentoService, IMapper mapper)
        {
            _orcamentoService = orcamentoService;
            _mapper = mapper;
        }

        [HttpPost("orcamentos")]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrcamentoDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Orcamento>(ModelState.GetErrors()));
            }

            var orcamento = _mapper.Map<Orcamento>(model);

            try
            {
                await _orcamentoService.CreateAsync(orcamento);
                return Created($"api/orcamentos/{orcamento.Id}", new ResultViewModel<Orcamento>(orcamento));
            }
            catch (MongoWriteException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível Salvar os Dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpGet("orcamentos")]
        public async Task<ActionResult<Orcamento>> GetAsync()
        {
            try
            {
                var orcamentos = await _orcamentoService.GetAsync();

                return Ok(new ResultViewModel<List<Orcamento>>(orcamentos));
            }
            catch (MongoException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível buscar os dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpGet("orcamentos/{id:length(24)}")]
        public async Task<ActionResult<Orcamento>> GetByIdAsync(string id)
        {
            try
            {
                var orcamento = await _orcamentoService.GetByIdAsync(id);

                if (orcamento.Produtos is not null)
                {
                    double totalGeral = 0;
                    foreach (var item in orcamento.Produtos)
                    {
                        var totalOrcamento = item.Quantidade * item.PrecoVenda;
                        totalGeral += totalOrcamento;
                    }
                    orcamento.Total = totalGeral;
                }

                return Ok(new ResultViewModel<Orcamento>(orcamento));
            }
            catch (MongoException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível buscar os dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpPut("orcamentos/{id:length(24)}")]
        public async Task<IActionResult> AtualizarOrcamentoAsync(string id, [FromBody] Orcamento orcamento)
        {
            try
            {
                var result = await _orcamentoService.GetByIdAsync(id);

                if (result == null)
                    return NotFound(new ResultViewModel<string>("Orçamento não encontrado"));

                await _orcamentoService.UpdateAsync(id, orcamento);
                return Ok(new ResultViewModel<Orcamento>(orcamento));
            }
            catch (MongoWriteException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível Atualizar os dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpPost("orcamentos/{id:length(24)}")]
        public async Task<IActionResult> AddProdutoOrcamentoAsync(string id, [FromBody] AddProdutoOrcamentoDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Orcamento>(ModelState.GetErrors()));
            }

            var produto = _mapper.Map<Produto>(model);

            try
            {
                var result = await _orcamentoService.GetByIdAsync(id);

                if (result == null)
                    return NotFound(new ResultViewModel<string>("Orçamento não encontrado"));

                await _orcamentoService.AddProdutoOrcamentoAsync(id, produto);
                return NoContent();
            }
            catch (MongoWriteException)
            {
                return StatusCode(500, new ResultViewModel<string>("Erro ao Adicionar Produto"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpPut("orcamentos/{id:length(24)}/{produtoId}")]
        public async Task<IActionResult> AtualizarProdutoAsync(string id, string produtoId, [FromBody] Produto produto)
        {

            try
            {
                var result = await _orcamentoService.GetByIdAsync(id);

                if (result == null)
                    return NotFound(new ResultViewModel<string>("Orçamento não encontrado"));

                await _orcamentoService.AtualizarProdutoOrcamento(id, produtoId, produto);
                return Ok();
            }
            catch (MongoWriteException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível Atualizar os dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpDelete("orcamentos/{id:length(24)}/{produtoId:length(24)}")]
        public async Task<IActionResult> RemoveProdutoAsync(string id, string produtoId)
        {
            try
            {
                var result = await _orcamentoService.GetByIdAsync(id);

                if (result == null)
                    return NotFound(new ResultViewModel<string>("Orçamento não encontrado"));

                await _orcamentoService.RemoveProdutoAsync(id, produtoId);
                return NoContent();
            }
            catch (MongoWriteException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível Remover os dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }

        [HttpDelete("orcamentos/{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _orcamentoService.DeleteAsync(id);
                return NoContent();
            }
            catch (MongoWriteException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível Atualizar os dados"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no Servidor"));
            }
        }
    }
}