// Infrastructure/Repository/Repositories/RepositoryCliente.cs
using Domain.Interfaces.InterfaceCliente;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        public async Task<bool> CPFJaExiste(string cpf, int? idIgnorar = null)
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                var query = data.Set<Cliente>().AsNoTracking()
                    .Where(c => c.CPF == cpf);

                if (idIgnorar.HasValue)
                    query = query.Where(c => c.Id != idIgnorar.Value);

                return await query.AnyAsync();
            }
        }

        public async Task<bool> EmailJaExiste(string email, int? idIgnorar = null)
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                var query = data.Set<Cliente>().AsNoTracking()
                    .Where(c => c.Email == email);

                if (idIgnorar.HasValue)
                    query = query.Where(c => c.Id != idIgnorar.Value);

                return await query.AnyAsync();
            }
        }

        public async Task<bool> TemAlocacoesVinculadas(int clienteId)
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                return await data.Set<Alocacao>().AnyAsync(a => a.IdCliente == clienteId);
            }
        }
    }
}