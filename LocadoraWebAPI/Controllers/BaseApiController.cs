using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseApiController : ControllerBase
    {
        // Padroniza retorno de sucesso com dados
        protected IActionResult Sucesso<T>(T data) => Ok(data);

        // Padroniza retorno de criação
        protected IActionResult Criado<T>(T data) => StatusCode(201, data);

        // Padroniza retorno de não encontrado
        protected IActionResult NaoEncontrado(string entidade) =>
            NotFound(new { mensagem = $"{entidade} não encontrado(a)." });

        // Padroniza retorno de erro de validação/negócio
        protected IActionResult ErroBusiness(string mensagem) =>
            BadRequest(new { mensagem });
    }
}