using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCarro
{
    public interface ICarro : IGeneric<Carro>
    {
        Task<List<Carro>> ListarCarrosDisponiveis();
    }
}
