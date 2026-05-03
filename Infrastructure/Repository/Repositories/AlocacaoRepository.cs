// Infrastructure/Repository/Repositories/RepositoryAlocacao.cs
using Domain.Interfaces.InterfaceAlocacao;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class AlocacaoRepository : GenericRepository<Alocacao>, IAlocacao
    {
        public AlocacaoRepository(ContextBase context) : base(context) { }

        public async Task<List<Alocacao>> ListarComRelacionamentos()
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                return await data.Set<Alocacao>()
                    .Include(a => a.Carro)
                    .Include(a => a.Cliente)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}