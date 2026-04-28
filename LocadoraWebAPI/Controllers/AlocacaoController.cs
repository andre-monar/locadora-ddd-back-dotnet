using Application.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AlocacaoController : BaseApiController
    {
        private readonly IAlocacaoApp _alocacaoApp;

        public AlocacaoController(IAlocacaoApp alocacaoApp)
        {
            _alocacaoApp = alocacaoApp;
        }

        /// <summary>Retorna todas as alocações.</summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var alocacoes = await _alocacaoApp.Listar();
            return Sucesso(alocacoes);
        }

        /// <summary>Retorna uma alocação pelo ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var alocacao = await _alocacaoApp.BuscarPorId(id);
            if (alocacao == null) return NaoEncontrado("Alocação");
            return Sucesso(alocacao);
        }

        /// <summary>Cria uma nova alocação. Valida disponibilidade do veículo.</summary>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Alocacao alocacao)
        {
            await _alocacaoApp.Adicionar(alocacao);

            if (alocacao.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", alocacao.Notificacoes.Select(n => n.Mensagem)));

            return Criado(alocacao);
        }

        /// <summary>Atualiza uma alocação existente (ex: registrar devolução).</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Alocacao alocacao)
        {
            alocacao.Id = id;
            await _alocacaoApp.Atualizar(alocacao);

            if (alocacao.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", alocacao.Notificacoes.Select(n => n.Mensagem)));

            return Sucesso(alocacao);
        }

        /// <summary>Remove uma alocação pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var alocacao = await _alocacaoApp.BuscarPorId(id);
            if (alocacao == null) return NaoEncontrado("Alocação");

            await _alocacaoApp.Deletar(alocacao);
            return Sucesso(new { mensagem = "Alocação removida com sucesso." });
        }
    }
}