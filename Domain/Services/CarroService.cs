using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CarroService : ICarroService
    {

        private readonly ICarro _ICarro;

        public CarroService(ICarro ICarro)
        {
            _ICarro = ICarro;
        }

        public async Task AddCarro(Carro carro)
        {
            // Validações de formato (via Notifies)
            var validaModelo = carro.ValidarString(carro.Modelo, "Modelo");
            var validaMarca = carro.ValidarString(carro.Marca, "Marca");
            var validaPlaca = carro.ValidarPlaca(carro.Placa, "Placa");
            var validaCor = carro.ValidarString(carro.Cor, "Cor");
            // apenas valores pos 1990:
            var validaAno = carro.ValidarInt(carro.Ano, "Ano", minimo: 1990); 
            var validaCategoria = carro.ValidarInt(carro.IdCategoria, "Categoria", minimo: 1);
            var validaImagemUrl = carro.ValidarString(carro.ImagemUrl, "URL da Imagem", obrigatorio: false);

            // Regra de domínio: ano máximo
            if (carro.Ano > DateTime.Now.Year + 1)
                carro.Notificacoes.Add(new Notifies { NomePropriedade = "Ano", Mensagem = "Ano não pode ser maior que o ano atual + 1" });

            if (validaModelo && validaMarca && validaPlaca && validaCor && validaAno && validaCategoria && validaImagemUrl
                && !carro.Notificacoes.Any())
            {
                carro.DataAlteracao = DateTime.Now;
                await _ICarro.Add(carro);
            }
        }

        public async Task UpdateCarro(Carro carro)
        {
            var validaModelo = carro.ValidarString(carro.Modelo, "Modelo");
            var validaMarca = carro.ValidarString(carro.Marca, "Marca");
            var validaPlaca = carro.ValidarPlaca(carro.Placa, "Placa");
            var validaCor = carro.ValidarString(carro.Cor, "Cor");
            var validaAno = carro.ValidarInt(carro.Ano, "Ano", minimo: 1990);
            var validaCategoria = carro.ValidarInt(carro.IdCategoria, "Categoria", minimo: 1);
            var validaImagemUrl = carro.ValidarString(carro.ImagemUrl, "URL da Imagem", obrigatorio: false);

            if (carro.Ano > DateTime.Now.Year + 1)
                carro.Notificacoes.Add(new Notifies { NomePropriedade = "Ano", Mensagem = "Ano não pode ser maior que o ano atual + 1" });

            if (validaModelo && validaMarca && validaPlaca && validaCor && validaAno && validaCategoria && validaImagemUrl
                && !carro.Notificacoes.Any())
            {
                carro.DataAlteracao = DateTime.Now;
                await _ICarro.Update(carro);
            }
        }
    }
}
