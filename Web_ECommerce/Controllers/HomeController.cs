using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web_Locadora.Controllers
{
    // ─────────────────────────────────────────────────────────────
    //  HomeController
    //  Responsável apenas por servir a tela inicial (React SPA).
    //  Toda a navegação entre Cliente / Veículo / Alocação é feita
    //  pelo React no front — o MVC não precisa de rotas adicionais
    //  para essas seções enquanto usarmos a abordagem de SPA.
    //
    //  Se quiser SSR tradicional depois, basta criar Views para cada
    //  action e mover a lógica de dados para cá, chamando ApiGetList
    //  via BaseController.
    // ─────────────────────────────────────────────────────────────
    public class HomeController : BaseController
    {
        public HomeController(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        // GET /
        // GET /Home/Index
        // Retorna a View que hospeda o bundle React (wwwroot/index.html
        // ou a View Razor que carrega o script).
        public IActionResult Index()
        {
            return View();
        }

        // Ponto de verificação simples — útil durante o desenvolvimento
        // para confirmar que a API está respondendo.
        // GET /Home/ApiStatus
        public async Task<IActionResult> ApiStatus()
        {
            try
            {
                var client = CreateClient();
                var response = await client.GetAsync("/swagger/v1/swagger.json");
                ViewBag.ApiOk = response.IsSuccessStatusCode;
            }
            catch
            {
                ViewBag.ApiOk = false;
            }
            return View();
        }
    }
}
