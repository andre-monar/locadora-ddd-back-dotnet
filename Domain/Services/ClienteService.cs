// Domain/Services/ClienteService.cs

using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Notifications;
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
            var validaDataNasc = cliente.ValidarData(cliente.DataNascimento.ToDateTime(TimeOnly.MinValue), "Data de Nascimento");

            // Regras unicidade
            if (validaCPF && await _ICliente.CPFJaExiste(cliente.CPF))
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "CPF", Mensagem = "CPF já cadastrado" });

            if (validaEmail && await _ICliente.EmailJaExiste(cliente.Email))
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "Email", Mensagem = "E-mail já cadastrado" });

            if (validaNome && validaCPF && validaEmail && validaCelular && validaCEP && validaDataNasc)
            {
                cliente.DataCriacao = DateTime.UtcNow;
                cliente.DataAlteracao = DateTime.UtcNow;
                
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
            var validaDataNasc = cliente.ValidarData(cliente.DataNascimento.ToDateTime(TimeOnly.MinValue), "Data de Nascimento");

            // Regras de unicidade (ignora o próprio cliente)
            if (validaCPF && await _ICliente.CPFJaExiste(cliente.CPF, cliente.Id))
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "CPF", Mensagem = "CPF já cadastrado" });

            if (validaEmail && await _ICliente.EmailJaExiste(cliente.Email, cliente.Id))
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "Email", Mensagem = "E-mail já cadastrado" });

            if (validaCPF && validaNome && validaEmail && validaCelular && validaCEP && validaDataNasc)
            {
                cliente.DataAlteracao = DateTime.UtcNow;
                await _ICliente.Update(cliente);
            }
        }
    }
}