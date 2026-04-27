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
        // todo métodos específicos de Cliente, ex: ListarClientesPorNome, ListarClientesPorEmail, etc
    }
}
