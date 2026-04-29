using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceAlocacao;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AlocacaoApp : GenericApp<Alocacao>, IAlocacaoApp
    {
        private readonly IAlocacaoService _service;

        public AlocacaoApp(IAlocacao repo, IAlocacaoService service) : base(repo)
        {
            _service = service;
        }

        public override async Task Adicionar(Alocacao obj)
            => await _service.AddAlocacao(obj);

        public override async Task Atualizar(Alocacao obj)
            => await _service.UpdateAlocacao(obj);
    }
}