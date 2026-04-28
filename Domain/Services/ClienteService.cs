// Domain/Services/ClienteService.cs

using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ClienteService : IServiceCliente
    {
        private readonly ICliente _ICliente;

        public ClienteService(ICliente ICliente)
        {
            _ICliente = ICliente;
        }

        public async Task AddCliente(Cliente cliente)
        {
            // Validações de campo
            var validaCPF = cliente.ValidarCPF(cliente.CPF, "CPF");
            var validaCNH = cliente.ValidarCNH(cliente.CNH, "CNH");
            var validaNome = cliente.ValidarPropriedadeString(cliente.Nome, "Nome");
            var validaEmail = cliente.ValidarEmail(cliente.Email, "Email");
            var validaCelular = cliente.ValidarCelular(cliente.Celular, "Celular");
            var validaCEP = cliente.ValidarCEP(cliente.CEP, "CEP");

            // Validação de estado (bool, só precisa verificar se foi informado)
            // Estado é opcional, padrão pode ser true/false conforme regra

            if (validaCPF && validaCNH && validaNome && validaEmail && validaCelular && validaCEP)
            {
                cliente.DataAlteracao = DateTime.Now;
                cliente.Estado = true; // Ativo por padrão no cadastro
                await _ICliente.Add(cliente);
            }
            else
            {
                // As notificações já foram adicionadas nos métodos de validação
                // Você pode logar ou apenas retornar
            }
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            // Validações de campo
            var validaCPF = cliente.ValidarCPF(cliente.CPF, "CPF");
            var validaCNH = cliente.ValidarCNH(cliente.CNH, "CNH");
            var validaNome = cliente.ValidarPropriedadeString(cliente.Nome, "Nome");
            var validaEmail = cliente.ValidarEmail(cliente.Email, "Email");
            var validaCelular = cliente.ValidarCelular(cliente.Celular, "Celular");
            var validaCEP = cliente.ValidarCEP(cliente.CEP, "CEP");

            if (validaCPF && validaCNH && validaNome && validaEmail && validaCelular && validaCEP)
            {
                cliente.DataAlteracao = DateTime.Now;
                await _ICliente.Update(cliente);
            }
        }

        public async Task DeleteCliente(int id)
        {
            var cliente = await _ICliente.GetEntityById(id);
            if (cliente != null)
            {
                await _ICliente.Delete(cliente);
            }
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _ICliente.GetEntityById(id);
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            return await _ICliente.List();
        }

        public async Task<List<Cliente>> ListarClientesAtivos()
        {
            return await _ICliente.ListarClientesAtivos(); // Método que você precisa criar na interface
        }

        public async Task<bool> CPFJaExiste(string cpf, int? idIgnorar = null)
        {
            return await _ICliente.CPFJaExiste(cpf, idIgnorar);
        }
    }
}