using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : BaseApiController
    {
        private readonly IClienteApp _clienteApp;

        public ClienteController(IClienteApp clienteApp)
        {
            _clienteApp = clienteApp;
        }

        /// <summary>Retorna todos os clientes cadastrados.</summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _clienteApp.Listar();
            return Sucesso(clientes);
        }

        /// <summary>Retorna um cliente pelo ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var cliente = await _clienteApp.BuscarPorId(id);
            if (cliente == null) return NaoEncontrado("Cliente");
            return Sucesso(cliente);
        }

        /// <summary>Cria um novo cliente.</summary>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Cliente cliente)
        {
            await _clienteApp.Adicionar(cliente);

            if (cliente.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", cliente.Notificacoes.Select(n => n.Mensagem)));

            return Criado(cliente);
        }

        /// <summary>Atualiza um cliente existente.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Cliente cliente)
        {
            cliente.Id = id;
            await _clienteApp.Atualizar(cliente);

            if (cliente.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", cliente.Notificacoes.Select(n => n.Mensagem)));

            return Sucesso(cliente);
        }

        /// <summary>Remove um cliente pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _clienteApp.BuscarPorId(id);
            if (cliente == null) return NaoEncontrado("Cliente");

            await _clienteApp.Deletar(cliente);
            return Sucesso(new { mensagem = "Cliente removido com sucesso." });
        }
    }
}