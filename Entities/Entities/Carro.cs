using Entities.Entities.Enums;
using Entities.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{

    [Table("TB_CAR")]
    public class Carro : Notifies
    {
        [Column("CAR_ID")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CAR_MODELO")]
        [Display(Name = "Modelo")]
        [MaxLength(255)]
        public string Modelo { get; set; }

        [Column("CAR_MARCA")]
        [Display(Name = "Marca")]
        [MaxLength(150)]
        public string Marca { get; set; }

        [Column("CAR_PLACA")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Column("CAR_GRUPO")]
        [Display(Name = "Grupo")]
        public GrupoEnum Grupo { get; set; }

        [Column("CAR_DATA_ALTERACAO")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        [NotMapped]
        public IFormFile Imagem { get; set; }
    }
}
