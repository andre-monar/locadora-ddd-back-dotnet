using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AlocacaoService : IServiceAlocacao
    {
        private readonly IAlocacao _ICompraUsuario;
        public AlocacaoService(IAlocacao ICompraUsuario)
        {
            _ICompraUsuario = ICompraUsuario;
        }

        public Task<Alocacao> AtualizarStatus(int alocacaoId, AlocacaoStatusEnum status)
        {
            // pra atualizarmos o status basta fazer o codigo: 
            // e ai atualizar o status e depois chamar o método de update do repositório
            // assim: 
        }
    }
}
