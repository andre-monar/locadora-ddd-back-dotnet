using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceAlocacao
{
    public interface IAlocacao : IGeneric<Alocacao>
    {
        Task<List<Alocacao>> ListarComRelacionamentos();
        Task<Alocacao> GetByIdNoTracking(int id); // sem tracking para verificar se carro mudou sem conflito
    }
}
