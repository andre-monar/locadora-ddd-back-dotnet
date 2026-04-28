using ApplicationApp.Interfaces;
using Domain.Interfaces.Generics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class GenericApp<T> : IGenericApp<T> where T : class
    {
        private readonly IGeneric<T> _repo;

        public GenericApp(IGeneric<T> repo)
        {
            _repo = repo;
        }

        public async Task Adicionar(T objeto) =>
            await _repo.Add(objeto);

        public async Task Atualizar(T objeto) =>
            await _repo.Update(objeto);

        public async Task Deletar(T objeto) =>
            await _repo.Delete(objeto);

        public async Task<T> BuscarPorId(int id) =>
            await _repo.GetEntityById(id);

        public async Task<List<T>> Listar() =>
            await _repo.List();
    }
}