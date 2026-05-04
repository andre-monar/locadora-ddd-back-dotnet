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
        public CarroRepository(ContextBase context) : base(context) { }

        public async Task<List<Carro>> ListarCarrosDisponiveis()
        {
            // Carro disponível se não houver alocação com status Ativo
            return await _context.Set<Carro>()
                .Include(c => c.Categoria)
                .Where(c => c.Ativo && !_context.Set<Alocacao>()
                    .Any(a => a.IdCarro == c.Id && a.Status == AlocacaoStatusEnum.Ativo))
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<Carro>> ListarCarrosComCategoria()
        {
            return await _context.Set<Carro>()
                .Include(c => c.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> PlacaJaExiste(string placa, int? idIgnorar = null)
        {
            var query = _context.Set<Carro>().AsNoTracking()
                .Where(c => c.Placa == placa);

            if (idIgnorar.HasValue)
                query = query.Where(c => c.Id != idIgnorar.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> TemAlocacoesVinculadas(int carroId)
        {
            return await _context.Set<Alocacao>().AnyAsync(a => a.IdCarro == carroId);
        }

        public async Task<bool> TemAlocacaoAtiva(int carroId)
        {
            return await _context.Set<Alocacao>()
                .AnyAsync(a => a.IdCarro == carroId &&
                    (a.Status == AlocacaoStatusEnum.Ativo));
        }
    }
}