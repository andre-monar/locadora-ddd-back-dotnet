using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class ClienteApp : GenericApp<Cliente>, IClienteApp
    {
        private readonly IClienteService _service;

        public ClienteApp(ICliente repo, IClienteService service) : base(repo)
        {
            _service = service;
        }

        // Passa pelo Service porque tem validações (CPF, CNH, campos obrigatórios)
        public override async Task Adicionar(Cliente cliente) =>
            await _service.AddCliente(cliente);

        public override async Task Atualizar(Cliente cliente) =>
            await _service.UpdateCliente(cliente);

        // Deletar, BuscarPorId e Listar herdam do GenericApp — sem lógica extra
    }
}