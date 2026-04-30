
using Domain.Interfaces.InterfaceAlocacao;
using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Entities.Enums;
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

        public async Task AddAlocacao(Alocacao alocacao)
        {
            // Validações de formato
            var validaCarro = alocacao.ValidarInt(alocacao.IdCarro, "Carro", minimo: 1);
            var validaCliente = alocacao.ValidarInt(alocacao.IdCliente, "Cliente", minimo: 1);
            var validaDataRetirada = alocacao.ValidarData(alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue), "Data de Retirada");
            var validaDataPrevista = alocacao.ValidarData(alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue), "Data Prevista de Devolução");
            var validaDatas = alocacao.ValidarDataMaior(alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue), alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue), "Data Prevista de Devolução");

            // Regra de domínio: calcular ValorTotal
            if (validaCarro && validaCliente)
            {
                var carro = await _ICarro.GetEntityById(alocacao.IdCarro);
                if (carro != null)
                {
                    var categoria = await _ICategoriaCarro.GetEntityById(carro.IdCategoria);
                    if (categoria != null)
                    {
                        int dias = (alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue) - alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue)).Days;
                        alocacao.ValorTotal = dias * categoria.ValorDiaria;
                    }
                }
            }

            if (validaCarro && validaCliente && validaDataRetirada && validaDataPrevista && validaDatas
                && !alocacao.Notificacoes.Any())
            {
                alocacao.Status = AlocacaoStatusEnum.Ativo;
                alocacao.DataCriacao = DateTime.UtcNow;
                alocacao.DataAlteracao = DateTime.UtcNow;
                await _IAlocacao.Add(alocacao);
            }
        }

        public async Task UpdateAlocacao(Alocacao alocacao)
        {
            // Validações de formato
            var validaCarro = alocacao.ValidarInt(alocacao.IdCarro, "Carro", minimo: 1);
            var validaCliente = alocacao.ValidarInt(alocacao.IdCliente, "Cliente", minimo: 1);
            var validaDataRetirada = alocacao.ValidarData(alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue), "Data de Retirada");
            var validaDataPrevista = alocacao.ValidarData(alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue), "Data Prevista de Devolução");
            var validaDatas = alocacao.ValidarDataMaior(alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue), alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue), "Data Prevista de Devolução");

            // Regra de domínio: calcular ValorTotal
            if (validaCarro && validaCliente)
            {
                var carro = await _ICarro.GetEntityById(alocacao.IdCarro);
                if (carro != null)
                {
                    var categoria = await _ICategoriaCarro.GetEntityById(carro.IdCategoria);
                    if (categoria != null)
                    {
                        int dias = (alocacao.DataPrevistaDevolucao.ToDateTime(TimeOnly.MinValue) - alocacao.DataRetirada.ToDateTime(TimeOnly.MinValue)).Days;
                        alocacao.ValorTotal = dias * categoria.ValorDiaria;
                    }
                }
            }

            if (validaCarro && validaCliente && validaDataRetirada && validaDataPrevista && validaDatas
                && !alocacao.Notificacoes.Any())
            {
                alocacao.Status = AlocacaoStatusEnum.Ativo;
                alocacao.DataCriacao = DateTime.UtcNow;
                alocacao.DataAlteracao = DateTime.UtcNow;
                await _IAlocacao.Update(alocacao);
            }
        }
    }
}