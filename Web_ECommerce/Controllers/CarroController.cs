using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Entities;

namespace Web_Locadora.Controllers
{
    public class CarroController : BaseController
    {
        private const string Endpoint = "api/carro";

        public CarroController(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        public async Task<IActionResult> Index()
        {
            var carros = await ApiGetList<Carro>(Endpoint);
            return View(carros);
        }

        public IActionResult Registro() => View(new Carro());

        public async Task<IActionResult> Editar(int id)
        {
            var carro = await ApiGetById<Carro>(Endpoint, id);
            if (carro == null) return RedirectToAction(nameof(Index));
            return View("Registro", carro);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Carro carro)
        {
            if (!ModelState.IsValid) return View("Registro", carro);

            bool sucesso = carro.Id == 0
                ? await ApiPost(Endpoint, carro)
                : await ApiPut(Endpoint, carro.Id, carro);

            if (sucesso) TempData["Sucesso"] = "Carro salvo com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            await ApiDelete(Endpoint, id);
            TempData["Sucesso"] = "Carro removido.";
            return RedirectToAction(nameof(Index));
        }
    }
}