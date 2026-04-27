using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        [ForeignKey("TB_CARRO")]
        [Column(Order = 1)]
        public int IdCarro { get; set; }
        public virtual Carro Carro { get; set; }

        [Display(Name = "Cliente")]
        [ForeignKey("TB_CLIENTE")]
        [Column(Order = 1)]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("ALO_STATUS")]
        [Display(Name = "Status")]
        public AlocacaoStatusEnum Status { get; set; }

        [Column("ALO_DATA_INICIO")]
        [Display(Name = "Data de Início")]
        public DateTime DataInicio { get; set; }

        [Column("ALO_DATA_DEVOLUCAO")]
        [Display(Name = "Data de Devolução")]
        public DateTime DataDevolucao { get; set; }

        [Column("ALO_DATA_FIM")]
        [Display(Name = "Data de Fim")]
        public DateTime DataFim { get; set; }

        [Column("ALO_DATA_ALTERACAO")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }


    }
}
