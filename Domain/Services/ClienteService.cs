// Domain/Services/ClienteService.cs

using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ICliente _ICliente;

        public ClienteService(ICliente ICliente)
        {
            _ICliente = ICliente;
        }

        public async Task AddCliente(Cliente cliente)
        {
            var validaNome = cliente.ValidarString(cliente.Nome, "Nome");
            var validaCPF = cliente.ValidarCPF(cliente.CPF, "CPF");
            var validaEmail = cliente.ValidarEmail(cliente.Email, "Email");
            var validaCelular = cliente.ValidarCelular(cliente.Celular, "Celular");
            var validaCEP = cliente.ValidarCEP(cliente.CEP, "CEP", obrigatorio: false); // opcional
            var validaDataNasc = cliente.ValidarData(cliente.DataNascimento, "Data de Nascimento");

            if (validaNome && validaCPF && validaEmail && validaCelular && validaCEP && validaDataNasc)
            {
                cliente.DataCriacao = DateTime.Now;
                cliente.DataAlteracao = DateTime.Now;
                await _ICliente.Add(cliente);
            }
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            var validaNome = cliente.ValidarString(cliente.Nome, "Nome");
            var validaCPF = cliente.ValidarCPF(cliente.CPF, "CPF");
            var validaEmail = cliente.ValidarEmail(cliente.Email, "Email");
            var validaCelular = cliente.ValidarCelular(cliente.Celular, "Celular");
            var validaCEP = cliente.ValidarCEP(cliente.CEP, "CEP", obrigatorio: false); // opcional
            var validaDataNasc = cliente.ValidarData(cliente.DataNascimento, "Data de Nascimento");

            if (validaCPF && validaNome && validaEmail && validaCelular && validaCEP && validaDataNascimento)
            {
                cliente.DataAlteracao = DateTime.Now;
                await _ICliente.Add(cliente);
            }
        }
    }
}