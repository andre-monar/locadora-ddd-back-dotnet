using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.InterfaceAlocacao;
using Entities.Entities;
using LocadoraWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaCarroController : BaseApiController
    {
        private readonly ICategoriaCarroApp _app;

        public CategoriaCarroController(ICategoriaCarroApp app)
        {
            _app = app;
        }

        ///<summary>Lista todas as categorias de carro.</summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _app.Listar();
            return Sucesso(lista);
        }

        ///<summary>Busca uma categoria pelo ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var obj = await _app.BuscarPorId(id);
            if (obj == null) return NaoEncontrado("Categoria");
            return Sucesso(obj);
        }

        ///<summary>Cria uma nova categoria de carro.</summary>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarCategoriaCarroDto dto)
        {
            var categoria = new CategoriaCarro
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                ValorDiaria = dto.ValorDiaria,
                Ativo = dto.Ativo
            };

            await _app.Adicionar(categoria);
            if (categoria.Notificacoes.Any())
                return ErroValidacao(categoria.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));

            return Criado(categoria);
        }

        ///<summary>Atualiza uma categoria existente pelo ID.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CriarCategoriaCarroDto dto)
        {
            var categoria = new CategoriaCarro
            {
                Id = id,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                ValorDiaria = dto.ValorDiaria,
                Ativo = dto.Ativo
            };

            await _app.Atualizar(categoria);
            if (categoria.Notificacoes.Any())
                return ErroValidacao(categoria.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));

            return Sucesso(categoria);
        }

        ///<summary>Remove uma categoria pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var categoria = await _app.BuscarPorId(id);
            if (categoria == null) return NaoEncontrado("Categoria");

            await _app.Deletar(categoria);

            if (categoria.Notificacoes.Any())
                return ErroValidacao(categoria.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));
            return Sucesso(new { mensagem = "Categoria removida com sucesso." });
        }
    }
}