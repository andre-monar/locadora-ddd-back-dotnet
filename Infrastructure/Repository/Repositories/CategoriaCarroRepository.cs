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
        public CategoriaCarroRepository(ContextBase context) : base(context) { }
        public async Task<bool> NomeJaExiste(string nome, int? idIgnorar = null)
        {
            var query = _context.Set<CategoriaCarro>().AsNoTracking()
                .Where(c => c.Nome == nome);

            if (idIgnorar.HasValue)
                query = query.Where(c => c.Id != idIgnorar.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> TemCarrosVinculados(int categoriaId)
        {
            return await _context.Set<Carro>().AnyAsync(c => c.IdCategoria == categoriaId);
        }

        public async Task<bool> TemCarrosAtivosVinculados(int categoriaId)
        {
            return await _context.Set<Carro>()
                .AnyAsync(c => c.IdCategoria == categoriaId && c.Ativo);
        }
    }
}