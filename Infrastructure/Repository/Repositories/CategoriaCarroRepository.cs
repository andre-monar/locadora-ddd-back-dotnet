// Infrastructure/Repository/Repositories/RepositoryCategoriaCarro.cs
using Domain.Interfaces.InterfaceCategoriaCarro;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class CategoriaCarroRepository : GenericRepository<CategoriaCarro>, ICategoriaCarro
    {
        public async Task<bool> NomeJaExiste(string nome, int? idIgnorar = null)
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                var query = data.Set<CategoriaCarro>().AsNoTracking()
                    .Where(c => c.Nome == nome);

                if (idIgnorar.HasValue)
                    query = query.Where(c => c.Id != idIgnorar.Value);

                return await query.AnyAsync();
            }
        }

        public async Task<bool> TemCarrosVinculados(int categoriaId)
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                return await data.Set<Carro>().AnyAsync(c => c.IdCategoria == categoriaId);
            }
        }
    }
}