// Infrastructure/Repository/Repositories/RepositoryCarro.cs
using Domain.Interfaces.InterfaceCarro;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class CarroRepository : GenericRepository<Carro>, ICarro
    {
        public async Task<List<Carro>> ListarCarrosDisponiveis()
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                // Carro disponível se não houver alocação com status Ativo
                return await data.Set<Carro>()
                    .Include(c => c.Categoria)
                    .Where(c => c.Ativo && !data.Set<Alocacao>()
                        .Any(a => a.IdCarro == c.Id && a.Status == AlocacaoStatusEnum.Ativo))
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<bool> PlacaJaExiste(string placa, int? idIgnorar = null)
        {
            using (var data = new ContextBase(new DbContextOptions<ContextBase>()))
            {
                var query = data.Set<Carro>().AsNoTracking()
                    .Where(c => c.Placa == placa);

                if (idIgnorar.HasValue)
                    query = query.Where(c => c.Id != idIgnorar.Value);

                return await query.AnyAsync();
            }
        }
    }
}