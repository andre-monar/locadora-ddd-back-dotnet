using Entities.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AlocacaoStatusController : BaseApiController
    {
        /// <summary>Retorna todos os status possíveis para alocação.</summary>
        [HttpGet]
        public IActionResult Listar()
        {
            var statusList = Enum.GetValues(typeof(AlocacaoStatusEnum))
                                 .Cast<AlocacaoStatusEnum>()
                                 .Select(e => new
                                 {
                                     value = (int)e,
                                     label = e.ToString()
                                 })
                                 .ToList();

            return Sucesso(statusList);
        }
    }
}