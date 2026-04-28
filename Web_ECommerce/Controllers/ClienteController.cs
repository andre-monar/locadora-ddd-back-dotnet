using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Entities;

namespace Web_Locadora.Controllers
{
    public class ClienteController : BaseController
    {
        private const string Endpoint = "api/cliente";

        public ClienteController(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        // GET /Cliente
        public async Task<IActionResult> Index()
        {
            var clientes = await ApiGetList<Cliente>(Endpoint);
            return View(clientes);
        }

        // GET /Cliente/Registro
        public IActionResult Registro() => View(new Cliente());

        // GET /Cliente/Registro/5
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await ApiGetById<Cliente>(Endpoint, id);
            if (cliente == null) return RedirectToAction(nameof(Index));
            return View("Registro", cliente);
        }

        // POST /Cliente/Salvar
        [HttpPost]
        public async Task<IActionResult> Salvar(Cliente cliente)
        {
            if (!ModelState.IsValid) return View("Registro", cliente);

            bool sucesso = cliente.Id == 0
                ? await ApiPost(Endpoint, cliente)
                : await ApiPut(Endpoint, cliente.Id, cliente);

            if (sucesso) TempData["Sucesso"] = "Cliente salvo com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST /Cliente/Excluir/5
        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            await ApiDelete(Endpoint, id);
            TempData["Sucesso"] = "Cliente removido.";
            return RedirectToAction(nameof(Index));
        }
    }
}