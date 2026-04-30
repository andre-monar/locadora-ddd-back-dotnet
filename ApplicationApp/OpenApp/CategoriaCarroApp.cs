// ApplicationApp/OpenApp/CategoriaCarroApp.cs
using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class CategoriaCarroApp : GenericApp<CategoriaCarro>, ICategoriaCarroApp
    {
        private readonly ICategoriaCarroService _service;

        public CategoriaCarroApp(ICategoriaCarro repo, ICategoriaCarroService service) : base(repo) 
        {
            _service = service; 
        }

        // Passa pelo Service porque tem regras de negócio:
        // validação de Nome obrigatório, ValorDiaria > 0
        public override async Task Adicionar(CategoriaCarro categoria) =>
            await _service.AddCategoria(categoria);

        public override async Task Atualizar(CategoriaCarro categoria) =>
            await _service.UpdateCategoria(categoria);

        public override async Task Deletar(CategoriaCarro categoria)
            => await _service.DeleteCategoria(categoria);
        // BuscarPorId e Listar herdam do GenericApp
    }
}