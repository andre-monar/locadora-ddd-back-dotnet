using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AlocacaoApp : InterfaceCompraUsuarioApp
    {
        private readonly IAlocacao _ICompraUsuario;

        private readonly IServiceAlocacao _IServiceCompraUsuario;

        public AppCompraUsuario(IAlocacao ICompraUsuario, IServiceAlocacao IServiceCompraUsuario)
        {
            _ICompraUsuario = ICompraUsuario;
            _IServiceCompraUsuario = IServiceCompraUsuario;
        }


        public async Task<Alocacao> CarrinhoCompras(string userId)
        {
            return await _IServiceCompraUsuario.CarrinhoCompras(userId);
        }

        public async Task<Alocacao> ProdutosComprados(string userId)
        {
            return await _IServiceCompraUsuario.ProdutosComprados(userId);
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            return await _ICompraUsuario.ConfirmaCompraCarrinhoUsuario(userId);
        }




        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            return await _ICompraUsuario.QuantidadeProdutoCarrinhoUsuario(userId);
        }


        public async Task Add(Alocacao Objeto)
        {
            await _ICompraUsuario.Add(Objeto);
        }

        public async Task Delete(Alocacao Objeto)
        {
            await _ICompraUsuario.Delete(Objeto);
        }

        public async Task<Alocacao> GetEntityById(int Id)
        {
            return await _ICompraUsuario.GetEntityById(Id);
        }

        public async Task<List<Alocacao>> List()
        {
            return await _ICompraUsuario.List();
        }



        public async Task Update(Alocacao Objeto)
        {
            await _ICompraUsuario.Update(Objeto);
        }


    }
}
