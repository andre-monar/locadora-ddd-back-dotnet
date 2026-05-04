using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarro _ICarro;
        private readonly ICategoriaCarro _ICategoriaCarro;

        public CarroService(ICarro ICarro, ICategoriaCarro ICategoriaCarro)
        {
            _ICarro = ICarro;
            _ICategoriaCarro = ICategoriaCarro;
        }

        private async Task<bool> ValidarCarro(Carro carro, int? idIgnorar = null)
        {
            var validaModelo = carro.ValidarString(carro.Modelo, "Modelo");
            var validaMarca = carro.ValidarString(carro.Marca, "Marca");
            var validaPlaca = carro.ValidarPlaca(carro.Placa, "Placa");
            var validaCor = carro.ValidarString(carro.Cor, "Cor");
            var validaAno = carro.ValidarInt(carro.Ano, "Ano", minimo: 1990);
            var validaCategoria = carro.ValidarInt(carro.IdCategoria, "Categoria", minimo: 1);
            var validaImagemUrl = carro.ValidarString(carro.ImagemUrl, "ImagemUrl", obrigatorio: false);

            if (validaPlaca && await _ICarro.PlacaJaExiste(carro.Placa, idIgnorar))
                carro.Notificacoes.Add(new Notifies { NomePropriedade = "Placa", Mensagem = "Placa já cadastrada" });

            if (carro.Ano > DateTime.Now.Year)
                carro.Notificacoes.Add(new Notifies { NomePropriedade = "Ano", Mensagem = "Ano não pode ser maior que o ano atual" });

            // Carros ativos só podem ser vinculados à categorias ativas
            if (validaCategoria && carro.Ativo)
            {
                var categoria = await _ICategoriaCarro.GetEntityById(carro.IdCategoria);
                if (categoria == null || !categoria.Ativo)
                    carro.Notificacoes.Add(new Notifies { NomePropriedade = "IdCategoria", Mensagem = "A categoria deve ser ativa para carros ativos." });
            }

            // Só podemos inativar carros que estão disponíveis (sem alocações ativas):
            if (!carro.Ativo)
            {
                var temAlocacaoAtiva = await _ICarro.TemAlocacaoAtiva(carro.Id);
                if (temAlocacaoAtiva)
                    carro.Notificacoes.Add(new Notifies { NomePropriedade = "Ativo", Mensagem = "Não é possível inativar um carro com alocações ativas" });
            }

            // Atualiza disponibilidade
            carro.Disponivel = !await _ICarro.TemAlocacaoAtiva(carro.Id);

            return validaModelo && validaMarca && validaPlaca && validaCor && validaAno && validaCategoria && validaImagemUrl
                && !carro.Notificacoes.Any();
        }

        public async Task AddCarro(Carro carro)
        {
            if (await ValidarCarro(carro))
            {
                carro.DataCriacao = DateTime.UtcNow;
                carro.DataAlteracao = DateTime.UtcNow;
                await _ICarro.Add(carro);
            }
        }

        public async Task UpdateCarro(Carro carro)
        {
            if (await ValidarCarro(carro, carro.Id))
            {
                carro.DataAlteracao = DateTime.UtcNow;
                await _ICarro.Update(carro);
            }
        }

        public async Task DeleteCarro(Carro carro)
        {
            var temAlocacoes = await _ICarro.TemAlocacoesVinculadas(carro.Id);
            if (temAlocacoes)
            {
                carro.Notificacoes.Add(new Notifies { NomePropriedade = "Cancelado", Mensagem = "Não é possível excluir este carro pois existem alocações vinculadas." });
                return;
            }
            await _ICarro.Delete(carro);
        }
    }
}