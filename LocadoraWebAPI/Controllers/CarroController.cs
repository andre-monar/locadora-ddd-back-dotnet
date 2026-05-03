using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Entities.Entities;
using LocadoraWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            var carros = await _carroApp.ListarComCategoria();
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
        public async Task<IActionResult> Criar([FromBody] CriarCarroDto dto)
        {
            var carro = new Carro
            {
                Modelo = dto.Modelo,
                Marca = dto.Marca,
                Placa = dto.Placa,
                Ano = dto.Ano,
                Cor = dto.Cor,
                ImagemUrl = dto.ImagemUrl,
                IdCategoria = dto.IdCategoria,
                Ativo = dto.Ativo
            };

            await _carroApp.Adicionar(carro);

            if (carro.Notificacoes.Any())
                return ErroValidacao(carro.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));
            // Recarrega com categoria
            carro = await _carroApp.BuscarPorId(carro.Id);

            return Criado(carro);
        }

        /// <summary>Atualiza um veículo existente.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CriarCarroDto dto)
        {
            var carro = new Carro
            {
                Id = id,
                Modelo = dto.Modelo,
                Marca = dto.Marca,
                Placa = dto.Placa,
                Ano = dto.Ano,
                Cor = dto.Cor,
                ImagemUrl = dto.ImagemUrl,
                IdCategoria = dto.IdCategoria,
                Ativo = dto.Ativo
            };

            await _carroApp.Atualizar(carro);
            

            if (carro.Notificacoes.Any())
                return ErroValidacao(carro.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));
            // Recarrega com categoria
            carro = await _carroApp.BuscarPorId(carro.Id);
            return Sucesso(carro);
        }

        /// <summary>Remove um veículo pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var carro = await _carroApp.BuscarPorId(id);
            if (carro == null) return NaoEncontrado("Carro");

            await _carroApp.Deletar(carro);

            if (carro.Notificacoes.Any())
                return ErroValidacao(carro.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));
            return Sucesso(new { mensagem = "Carro removido com sucesso." });
        }
    }
}