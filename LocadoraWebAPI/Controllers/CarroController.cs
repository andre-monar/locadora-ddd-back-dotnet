using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CarroController : BaseApiController
    {
        private readonly ICarroApp _carroApp;

        public CarroController(ICarroApp carroApp)
        {
            _carroApp = carroApp;
        }

        /// <summary>Retorna todos os veículos.</summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var carros = await _carroApp.Listar();
            return Sucesso(carros);
        }

        /// <summary>Retorna apenas veículos disponíveis (sem alocação ativa).</summary>
        [HttpGet("disponiveis")]
        public async Task<IActionResult> Disponiveis()
        {
            var carros = await _carroApp.ListarDisponiveis();
            return Sucesso(carros);
        }

        /// <summary>Retorna um veículo pelo ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var carro = await _carroApp.BuscarPorId(id);
            if (carro == null) return NaoEncontrado("Veículo");
            return Sucesso(carro);
        }

        /// <summary>Cadastra um novo veículo.</summary>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Carro carro)
        {
            await _carroApp.Adicionar(carro);

            if (carro.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", carro.Notificacoes.Select(n => n.Mensagem)));

            return Criado(carro);
        }

        /// <summary>Atualiza um veículo existente.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Carro carro)
        {
            carro.Id = id;
            await _carroApp.Atualizar(carro);

            if (carro.Notificacoes.Any())
                return ErroBusiness(string.Join(", ", carro.Notificacoes.Select(n => n.Mensagem)));

            return Sucesso(carro);
        }

        /// <summary>Remove um veículo pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var carro = await _carroApp.BuscarPorId(id);
            if (carro == null) return NaoEncontrado("Veículo");

            await _carroApp.Deletar(carro);
            return Sucesso(new { mensagem = "Veículo removido com sucesso." });
        }
    }
}