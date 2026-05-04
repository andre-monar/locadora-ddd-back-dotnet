using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCliente
{
    public interface ICliente : IGeneric<Cliente>
    {
        Task<bool> CPFJaExiste(string cpf, int? idIgnorar = null);
        Task<bool> EmailJaExiste(string email, int? idIgnorar = null);
        Task<bool> TemAlocacoesVinculadas(int clienteId);
        Task<bool> TemAlocacoesAtivas(int clienteId);
    }
}
