using Entities.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarAlocacaoDto
    {
        [Required(ErrorMessage = "Carro é obrigatório")]
        public int IdCarro { get; set; }

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public AlocacaoStatusEnum Status { get; set; }

        [Required(ErrorMessage = "Data de Retirada é obrigatória")]
        public DateOnly DataRetirada { get; set; }

        public DateOnly? DataDevolucao { get; set; }

        [Required(ErrorMessage = "Data Prevista de Devolução é obrigatória")]
        public DateOnly DataPrevistaDevolucao { get; set; }
    }
}