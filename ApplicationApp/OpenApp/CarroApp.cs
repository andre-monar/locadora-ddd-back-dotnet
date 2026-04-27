using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class CarroApp : InterfaceProductApp
    {
        ICarro _IProduct;
        IServiceCarro _IServiceProduct;
        public CarroApp(ICarro IProduct, IServiceCarro IServiceProduct)
        {
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }


        public async Task<List<Carro>> ListarProdutosCarrinhoUsuario(string userId)
        {
            return await _IProduct.ListarProdutosCarrinhoUsuario(userId);
        }

        public async Task<Carro> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            return await _IProduct.ObterProdutoCarrinho(idProdutoCarrinho);
        }



        public async Task AddProduct(Carro produto)
        {
            await _IServiceProduct.AddProduct(produto);
        }
        public async Task UpdateProduct(Carro produto)
        {
            await _IServiceProduct.UpdateProduct(produto);
        }

        public async Task<List<Carro>> ListarProdutosUsuario(string userId)
        {
            return await _IProduct.ListarProdutosUsuario(userId);
        }


        public async Task Add(Carro Objeto)
        {
            await _IProduct.Add(Objeto);
        }
        public async Task Delete(Carro Objeto)
        {
            await _IProduct.Delete(Objeto);
        }
        public async Task<Carro> GetEntityById(int Id)
        {
            return await _IProduct.GetEntityById(Id);
        }

        public async Task<List<Carro>> List()
        {
            return await _IProduct.List();
        }

        public async Task Update(Carro Objeto)
        {
            await _IProduct.Update(Objeto);
        }

        public async Task<List<Carro>> ListarProdutosComEstoque(string descricao)
        {
            return await _IServiceProduct.ListarProdutosComEstoque(descricao);
        }


    }
}
