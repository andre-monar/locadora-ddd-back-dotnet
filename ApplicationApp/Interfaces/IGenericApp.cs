using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGenericApp<T> where T : class
{
    Task Adicionar(T objeto);
    Task Atualizar(T objeto);
    Task Deletar(T objeto);
    Task<T> BuscarPorId(int id);
    Task<List<T>> Listar();
}