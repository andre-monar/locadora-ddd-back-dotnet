using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CarroService : IServiceCarro
    {

        private readonly ICarro _ICarro;

        public CarroService(ICarro ICarro)
        {
            _ICarro = ICarro;
        }

        public async Task AddCarro(Carro carro)
        {
            var validaModelo = carro.ValidarPropriedadeString(carro.Modelo, "Modelo");
            var validaMarca = carro.ValidarPropriedadeString(carro.Marca, "Marca");
            var validaPlaca = carro.ValidarPropriedadeString(carro.Placa, "Placa");
            // grupo n precisa, enum "autovalida"
            
            if (validaModelo && validaMarca && validaPlaca)
            {
                carro.DataAlteracao = DateTime.Now;
                await _ICarro.Add(carro);
            }
            // todo validação imagem?url?
        }

        //public async Task<List<Carro>> ListarProdutosComEstoque(string descricao)
        //{
        //    if (string.IsNullOrWhiteSpace(descricao))
        //        return await _IProduct.ListarProdutos(p => p.QtdEstoque > 0);
        //    else
        //    {
        //        return await _IProduct.ListarProdutos(p => p.QtdEstoque > 0
        //        && p.Nome.ToUpper().Contains(descricao.ToUpper()));
        //    }
        //}

        public async Task UpdateCarro(Carro carro)
        {
            var validaModelo = carro.ValidarPropriedadeString(carro.Modelo, "Modelo");
            var validaMarca = carro.ValidarPropriedadeString(carro.Marca, "Marca");
            var validaPlaca = carro.ValidarPropriedadeString(carro.Placa, "Placa");
            // grupo n precisa, enum "autovalida"

            if (validaModelo && validaMarca && validaPlaca)
            {
                carro.DataAlteracao = DateTime.Now;
                await _ICarro.Update(carro);
            }
            // todo validação imagem?url?
        }
    }
}
