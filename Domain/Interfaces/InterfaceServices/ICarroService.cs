using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface ICarroService
    {
        Task AddCarro(Carro carro);
        Task UpdateCarro(Carro carro);
        Task DeleteCarro(Carro carro);
    }
}