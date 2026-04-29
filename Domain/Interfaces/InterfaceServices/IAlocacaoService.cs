using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IAlocacaoService
    {
        Task AddAlocacao(Alocacao alocacao);
        Task UpdateAlocacao(Alocacao alocacao);
    }
}

