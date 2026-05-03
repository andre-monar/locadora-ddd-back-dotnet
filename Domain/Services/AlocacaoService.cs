using Domain.Interfaces.InterfaceAlocacao;
using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AlocacaoService : IAlocacaoService
    {
        private readonly IAlocacao _IAlocacao;
        private readonly ICarro _ICarro;
        private readonly ICategoriaCarro _ICategoriaCarro;

        public AlocacaoService(IAlocacao IAlocacao, ICarro ICarro, ICategoriaCarro ICategoriaCarro)
        {
            _IAlocacao = IAlocacao;
            _ICarro = ICarro;
            _ICategoriaCarro = ICategoriaCarro;
        }

        private async Task<bool> ValidarAlocacao(Alocacao alocacao, bool isUpdate = false, bool validarDisponibilidade = true)
        {
            var validaCarro = alocacao.ValidarInt(alocacao.IdCarro, "IdCarro", minimo: 1);
            var validaCliente = alocacao.ValidarInt(alocacao.IdCliente, "IdCliente", minimo: 1);
            var validaDataRetirada = alocacao.ValidarData(alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue), "DataRetirada", true);
            var validaDataPrevista = alocacao.ValidarData(alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue), "DataPrevistaDevolucao", true);
            var validaDatas = alocacao.ValidarDataMaior(alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue), alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue), "DataPrevistaDevolucao");

            // apenas alocações para carros ativos
            if (validaCarro)
            {
                var carro = await _ICarro.GetEntityById(alocacao.IdCarro);
                if (carro == null)
                    alocacao.Notificacoes.Add(new Notifies { NomePropriedade = "IdCarro", Mensagem = "Carro não encontrado" });
                else if (!carro.Ativo)
                    alocacao.Notificacoes.Add(new Notifies { NomePropriedade = "IdCarro", Mensagem = "Carro inativo não pode ser alocado" });
                else if (validarDisponibilidade && !carro.Disponivel)
                    alocacao.Notificacoes.Add(new Notifies { NomePropriedade = "IdCarro", Mensagem = "Carro não está disponível para alocação" });
            }

            // Calcula ValorTotal só se status for Retornado
            if (validaCarro && validaCliente)
            {
                if (alocacao.Status == AlocacaoStatusEnum.Retornado)
                {
                    var carro = await _ICarro.GetEntityById(alocacao.IdCarro);
                    if (carro != null)
                    {
                        var categoria = await _ICategoriaCarro.GetEntityById(carro.IdCategoria);
                        if (categoria != null)
                        {
                            int dias = (alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue)
                                       - alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue)).Days;
                            alocacao.ValorTotal = dias * categoria.ValorDiaria;
                        }
                    }
                }
                else
                {
                    alocacao.ValorTotal = null;
                }
            }
            return validaCarro && validaCliente && validaDataRetirada && validaDataPrevista && validaDatas
                && !alocacao.Notificacoes.Any();
        }

        public async Task AddAlocacao(Alocacao alocacao)
        {
            if (await ValidarAlocacao(alocacao))
            {
                alocacao.Status = AlocacaoStatusEnum.Ativo;
                alocacao.DataCriacao = DateTime.UtcNow;
                alocacao.DataAlteracao = DateTime.UtcNow;

                await _IAlocacao.Add(alocacao);

                // Atualiza disponibilidade do carro
                var carro = await _ICarro.GetEntityById(alocacao.IdCarro);
                if (carro != null)
                {
                    carro.Disponivel = !await _ICarro.TemAlocacaoAtiva(carro.Id);
                    await _ICarro.Update(carro);
                }
            }
        }

        public async Task UpdateAlocacao(Alocacao alocacao)
        {
            // Limpa data de devolução se cancelado
            if (alocacao.Status == AlocacaoStatusEnum.Cancelado) { // cancelado
                alocacao.DataDevolucao = null;
            }
            // Busca a alocação original para saber qual carro estava vinculado
            // Isso é pra evitar checar se o carro tá disponível se ele é o da própria alocação
            var alocacaoOriginal = await _IAlocacao.GetEntityById(alocacao.Id);
            bool carroMudou = alocacaoOriginal == null || alocacaoOriginal.IdCarro != alocacao.IdCarro;

            if (await ValidarAlocacao(alocacao, isUpdate: true, validarDisponibilidade: carroMudou))
            {
                alocacao.DataAlteracao = DateTime.UtcNow;
                await _IAlocacao.Update(alocacao);

                // Atualiza disponibilidade do carro
                var carro = await _ICarro.GetEntityById(alocacao.IdCarro);
                if (carro != null)
                {
                    carro.Disponivel = !await _ICarro.TemAlocacaoAtiva(carro.Id);
                    await _ICarro.Update(carro);
                }
            }
        }
    }
}