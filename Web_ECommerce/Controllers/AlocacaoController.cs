using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Entities;

namespace Web_Locadora.Controllers
{
    public class AlocacaoController : BaseController
    {
        private const string Endpoint = "api/alocacao";

        public AlocacaoController(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        public async Task<IActionResult> Index()
        {
            var alocacoes = await ApiGetList<Alocacao>(Endpoint);
            return View(alocacoes);
        }

        public async Task<IActionResult> Registro()
        {
            await CarregarSelects();
            return View(new Alocacao());
        }

        public async Task<IActionResult> Editar(int id)
        {
            var alocacao = await ApiGetById<Alocacao>(Endpoint, id);
            if (alocacao == null) return RedirectToAction(nameof(Index));
            await CarregarSelects();
            return View("Registro", alocacao);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Alocacao alocacao)
        {
            if (!ModelState.IsValid)
            {
                await CarregarSelects();
                return View("Registro", alocacao);
            }

            bool sucesso = alocacao.Id == 0
                ? await ApiPost(Endpoint, alocacao)
                : await ApiPut(Endpoint, alocacao.Id, alocacao);

            if (sucesso) TempData["Sucesso"] = "Alocação salva com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            await ApiDelete(Endpoint, id);
            TempData["Sucesso"] = "Alocação removida.";
            return RedirectToAction(nameof(Index));
        }

        // Popula ViewBag com listas para os <select> do formulário
        private async Task CarregarSelects()
        {
            ViewBag.Clientes = await ApiGetList<Cliente>("api/cliente");
            // Busca só carros disponíveis — endpoint que vamos criar na API
            ViewBag.Carros = await ApiGetList<Carro>("api/carro/disponiveis");
        }
    }
}