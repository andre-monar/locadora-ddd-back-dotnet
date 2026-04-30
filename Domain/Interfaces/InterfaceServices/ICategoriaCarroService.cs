using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface ICategoriaCarroService
    {
        Task AddCategoria(CategoriaCarro categoria);
        Task UpdateCategoria(CategoriaCarro categoria);
        Task DeleteCategoria(CategoriaCarro categoria);
    }
}