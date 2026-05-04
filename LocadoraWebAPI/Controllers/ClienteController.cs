using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceAlocacao;
using Entities.Entities;
using LocadoraWebAPI.Models;
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
        public async Task<IActionResult> Criar([FromBody] CriarClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                DataNascimento = dto.DataNascimento,
                Celular = dto.Celular,
                Email = dto.Email,
                CEP = dto.CEP,
                Endereco = dto.Endereco,
                ComplementoEndereco = dto.ComplementoEndereco,
                Ativo = dto.Ativo
            };

            await _clienteApp.Adicionar(cliente);

            if (cliente.Notificacoes.Any())
                return ErroValidacao(cliente.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));

            return Criado(cliente);
        }

        /// <summary>Atualiza um cliente existente.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CriarClienteDto dto)
        {
            var cliente = await _clienteApp.BuscarPorId(id);
            if (cliente == null) return NaoEncontrado("Cliente");

            cliente.Nome = dto.Nome;
            cliente.CPF = dto.CPF;
            cliente.DataNascimento = dto.DataNascimento;
            cliente.Celular = dto.Celular;
            cliente.Email = dto.Email;
            cliente.CEP = dto.CEP;
            cliente.Endereco = dto.Endereco;
            cliente.ComplementoEndereco = dto.ComplementoEndereco;
            cliente.Ativo = dto.Ativo;

            await _clienteApp.Atualizar(cliente);

            if (cliente.Notificacoes.Any())
                return ErroValidacao(cliente.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));

            return Sucesso(cliente);
        }

        /// <summary>Remove um cliente pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _clienteApp.BuscarPorId(id);
            if (cliente == null) return NaoEncontrado("Cliente");

            await _clienteApp.Deletar(cliente);

            if (cliente.Notificacoes.Any())
                return ErroValidacao(cliente.Notificacoes.Select(n => new { campo = n.NomePropriedade, mensagem = n.Mensagem }));
            return Sucesso(new { mensagem = "Cliente removido com sucesso." });
        }
    }
}