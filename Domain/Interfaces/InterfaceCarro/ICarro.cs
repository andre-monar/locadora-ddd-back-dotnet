using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface ICarro : IGeneric<Carro>
    {
        // todo: métodos específicos de Carro, ex: ListarCarrosDisponiveis, ListarCarrosPorMarca, etc
    }
}
