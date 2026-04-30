using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface IAlocacaoApp : IGenericApp<Alocacao>
    {
        Task<List<Alocacao>> ListarComRelacionamentos();
    }
}