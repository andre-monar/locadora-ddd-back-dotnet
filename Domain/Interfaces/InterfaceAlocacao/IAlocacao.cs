using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface IAlocacao : IGeneric<Alocacao>
    {
        public Task<Alocacao> ObterAlocacoesPorCliente(int clienteId);

        public Task<Alocacao> ObterAlocacoesPorStatus(int clienteId, AlocacaoStatusEnum status);

        public Task<bool> ObterDisponibilidadePorCarro(int carroId);
    }
}
