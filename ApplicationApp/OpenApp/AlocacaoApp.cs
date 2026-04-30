using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceAlocacao;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AlocacaoApp : GenericApp<Alocacao>, IAlocacaoApp
    {
        private readonly IAlocacaoService _service;
        private readonly IAlocacao _alocacaoRepo;
        public AlocacaoApp(IAlocacao repo, IAlocacaoService service) : base(repo)
        {
            _service = service;
            _alocacaoRepo = repo;
        }

        // Passa pelo Service porque tem regras de negócio:
        // cálculo de ValorTotal, validação de datas, status automático
        public override async Task Adicionar(Alocacao alocacao) =>
            await _service.AddAlocacao(alocacao);

        public override async Task Atualizar(Alocacao alocacao) =>
            await _service.UpdateAlocacao(alocacao);

        public async Task<List<Alocacao>> ListarComRelacionamentos()
            => await _alocacaoRepo.ListarComRelacionamentos();

        // Deletar, BuscarPorId herdam do GenericApp
    }
}