using Entities.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarAlocacaoDto
    {
        public int IdCarro { get; set; }

        public int IdCliente { get; set; }
        public AlocacaoStatusEnum Status { get; set; }

        public DateOnly DataRetirada { get; set; }

        public DateOnly? DataDevolucao { get; set; }
        public DateOnly DataPrevistaDevolucao { get; set; }
    }
}