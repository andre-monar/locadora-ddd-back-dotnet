using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericaApp<Carro>
    {
        Task AddProduct(Carro produto);
        Task UpdateProduct(Carro produto);

        Task<List<Carro>> ListarProdutosUsuario(string userId);

        Task<List<Carro>> ListarProdutosComEstoque(string descricao);

        Task<List<Carro>> ListarProdutosCarrinhoUsuario(string userId);

        Task<Carro> ObterProdutoCarrinho(int idProdutoCarrinho);
    }
}
