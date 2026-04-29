using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface ICategoriaCarroService
    {
        Task AddCategoria(CategoriaCarro categoria);
        Task UpdateCategoria(CategoriaCarro categoria);
    }
}