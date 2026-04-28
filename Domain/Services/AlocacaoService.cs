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

    }
}
