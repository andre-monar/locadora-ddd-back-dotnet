// Infrastructure/Repository/Repositories/RepositoryAlocacao.cs
using Domain.Interfaces.InterfaceAlocacao;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories
{
    public class AlocacaoRepository : GenericRepository<Alocacao>, IAlocacao
    {
    }
}