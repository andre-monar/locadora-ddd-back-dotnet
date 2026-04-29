using ApplicationApp.Interfaces;
using Entities.Entities;
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

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _app.Listar();
            return Sucesso(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var obj = await _app.BuscarPorId(id);
            if (obj == null) return NaoEncontrado("Categoria");
            return Sucesso(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CategoriaCarro categoria)
        {
            await _app.Adicionar(categoria);
            if (categoria.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", categoria.Notificacoes.Select(n => n.Mensagem)));

            return Criado(categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CategoriaCarro categoria)
        {
            categoria.Id = id;
            await _app.Atualizar(categoria);
            if (categoria.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", categoria.Notificacoes.Select(n => n.Mensagem)));

            return Sucesso(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var obj = await _app.BuscarPorId(id);
            if (obj == null) return NaoEncontrado("Categoria");

            await _app.Deletar(obj);
            return Sucesso(new { mensagem = "Categoria removida com sucesso." });
        }
    }
}