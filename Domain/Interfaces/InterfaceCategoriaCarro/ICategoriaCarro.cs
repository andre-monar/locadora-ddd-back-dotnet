using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCategoriaCarro
{
    public interface ICategoriaCarro : IGeneric<CategoriaCarro>
    {
        Task<bool> NomeJaExiste(string nome, int? idIgnorar = null);
        Task<bool> TemCarrosVinculados(int categoriaId);
        Task<bool> TemCarrosAtivosVinculados(int categoriaId);
    }
}