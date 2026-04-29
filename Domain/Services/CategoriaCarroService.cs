// Domain/Services/CategoriaCarroService.cs

using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoriaCarroService : ICategoriaCarroService
    {
        private readonly ICategoriaCarro _ICategoriaCarro;

        public CategoriaCarroService(ICategoriaCarro ICategoriaCarro)
        {
            _ICategoriaCarro = ICategoriaCarro;
        }

        public async Task AddCategoria(CategoriaCarro categoria)
        {
            var validaNome = categoria.ValidarString(categoria.Nome, "Nome");
            var validaDescricao = categoria.ValidarString(categoria.Descricao, "Descrição", obrigatorio: false);
            var validaValorDiaria = categoria.ValidarDecimal(categoria.ValorDiaria, "Valor da Diária", minimo: 0.01m);

            // verificar unicidade nome
            if (validaNome && await _ICategoriaCarro.NomeJaExiste(categoria.Nome))
                categoria.Notificacoes.Add(new Notifies { NomePropriedade = "Nome", Mensagem = "Nome já cadastrado" });

            if (validaNome && validaDescricao && validaValorDiaria)
            {
                await _ICategoriaCarro.Add(categoria);
            }
        }

        public async Task UpdateCategoria(CategoriaCarro categoria)
        {
            var validaNome = categoria.ValidarString(categoria.Nome, "Nome");
            var validaDescricao = categoria.ValidarString(categoria.Descricao, "Descrição", obrigatorio: false);
            var validaValorDiaria = categoria.ValidarDecimal(categoria.ValorDiaria, "Valor da Diária", minimo: 0.01m);
            
            // verificar unicidade nome
             if (validaNome && await _ICategoriaCarro.NomeJaExiste(categoria.Nome, categoria.Id))
                categoria.Notificacoes.Add(new Notifies { NomePropriedade = "Nome", Mensagem = "Nome já cadastrado" });

            if (validaNome && validaDescricao && validaValorDiaria)
            {
                await _ICategoriaCarro.Update(categoria);
            }
        }
    }
}