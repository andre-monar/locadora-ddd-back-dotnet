using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Entities
{

    [Table("TB_ALOCACAO")]
    public class Alocacao : Notifies
    {
        [Column("ALO_ID")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Carro")]
        [ForeignKey("Carro")]
        [Column(Order = 1)]
        [Required]
        public int IdCarro { get; set; }
        public virtual Carro Carro { get; set; }

        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        [Column(Order = 2)]
        [Required]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("ALO_STATUS")]
        [Display(Name = "Status")]
        [Required]
        public AlocacaoStatusEnum Status { get; set; }

        [Column("ALO_DATA_RETIRADA")]
        [Display(Name = "Data de Retirada")]
        [Required]
        public DateOnly DataRetirada { get; set; }

        [Column("ALO_DATA_DEVOLUCAO")]
        [Display(Name = "Data de Devolução")]
        public DateOnly? DataDevolucao { get; set; }

        [Column("ALO_DATA_PREVISTA_DEVOLUCAO")]
        [Display(Name = "Data Prevista de Devolução")]
        [Required]
        public DateOnly DataPrevistaDevolucao { get; set; }

        [Column("ALO_VALOR_TOTAL")]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Column("ALO_DATA_ALTERACAO")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        [Column("CAR_DATA_CRIACAO")]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
    }
}
