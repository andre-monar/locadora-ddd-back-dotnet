using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericaApp<Alocacao>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<Alocacao> CarrinhoCompras(string userId);

        public Task<Alocacao> ProdutosComprados(string userId);

        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);

    }
}
