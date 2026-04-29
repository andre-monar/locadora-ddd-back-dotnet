using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class CarroApp : IGenericApp<Carro>, ICarroApp
    {
        private readonly ICarroService _service;

        public CarroApp(ICarro repo, ICarroService service)
            : base(repo) // passa o repo pro AppGenerica
        {
            _service = service;
        }

        public async Task Adicionar(Carro carro) =>
            await _service.AddCarro(carro); // usa o service (tem validação)

        public async Task Atualizar(Carro carro) =>
            await _service.UpdateCarro(carro); // usa o service (tem validação)

        // ListarDisponiveis: método exclusivo do Carro
        // implementação vai no RepositoryCarro depois
        public async Task<List<Carro>> ListarDisponiveis() =>
            await _service.ListarDisponiveis();
    }
}