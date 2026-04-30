using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class CarroApp : GenericApp<Carro>, ICarroApp
    {
        private readonly ICarroService _service;
        private readonly ICarro _carroRepo;

        public CarroApp(ICarro repo, ICarroService service) : base(repo)
        {
            _carroRepo = repo;
            _service = service;
        }

        public override async Task Adicionar(Carro carro) =>
            await _service.AddCarro(carro);

        public override async Task Atualizar(Carro carro) =>
            await _service.UpdateCarro(carro);

        public async Task<List<Carro>> ListarDisponiveis() =>
            await _carroRepo.ListarCarrosDisponiveis();

        public async Task<List<Carro>> ListarComCategoria()
    => await _carroRepo.ListarCarrosComCategoria();
    }
}