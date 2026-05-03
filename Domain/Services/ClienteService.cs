// Domain/Services/ClienteService.cs

using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task<bool> ValidarCliente(Cliente cliente, int? idIgnorar = null)
        {
            var validaNome = cliente.ValidarString(cliente.Nome, "Nome");
            var validaCPF = cliente.ValidarCPF(cliente.CPF, "CPF");
            var validaEmail = cliente.ValidarEmail(cliente.Email, "Email");
            var validaCelular = cliente.ValidarCelular(cliente.Celular, "Celular");
            var validaCEP = cliente.ValidarCEP(cliente.CEP, "CEP", obrigatorio: false);
            var validaDataNasc = cliente.ValidarData(cliente.DataNascimento.ToDateTime(TimeOnly.MinValue), "DataNascimento");

            if (validaCPF && await _ICliente.CPFJaExiste(cliente.CPF, idIgnorar))
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "CPF", Mensagem = "CPF já cadastrado" });

            if (validaEmail && await _ICliente.EmailJaExiste(cliente.Email, idIgnorar))
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "Email", Mensagem = "E-mail já cadastrado" });

            if (validaDataNasc)
            {
                var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
                var idade = hoje.Year - cliente.DataNascimento.Year;
                if (cliente.DataNascimento > hoje.AddYears(-idade)) idade--;
                if (idade < 18)
                    cliente.Notificacoes.Add(new Notifies { NomePropriedade = "DataNascimento", Mensagem = "Cliente deve ter pelo menos 18 anos" });
            }

            return validaNome && validaCPF && validaEmail && validaCelular && validaCEP && validaDataNasc
                && !cliente.Notificacoes.Any();
        }

        public async Task AddCliente(Cliente cliente)
        {
            if (await ValidarCliente(cliente))
            {
                cliente.DataCriacao = DateTime.UtcNow;
                cliente.DataAlteracao = DateTime.UtcNow;
                await _ICliente.Add(cliente);
            }
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            if (await ValidarCliente(cliente, cliente.Id))
            {
                cliente.DataAlteracao = DateTime.UtcNow;
                await _ICliente.Update(cliente);
            }
        }
        public async Task DeleteCliente(Cliente cliente)
        {
            // Verifica se tem alocações vinculadas
            var temAlocacoes = await _ICliente.TemAlocacoesVinculadas(cliente.Id);
            if (temAlocacoes)
            {
                cliente.Notificacoes.Add(new Notifies { NomePropriedade = "Cancelado", Mensagem = "Não é possível excluir este cliente pois existem alocações vinculadas." });
                return;
            }

            await _ICliente.Delete(cliente);
        }
    }
}