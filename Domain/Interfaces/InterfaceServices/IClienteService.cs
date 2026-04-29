using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IClienteService
    {
        Task AddCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
    }
}