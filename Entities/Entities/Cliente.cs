using Entities.Entities.Enums;
using Entities.Notifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Cliente : Notifies
    {
        [Column("CLI_ID")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CLI_CPF")]
        [MaxLength(50)]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Column("CLI_CNH")]
        [MaxLength(50)]
        [Display(Name = "CNH")]
        public string CNH { get; set; }

        [Column("CLI_NOME")]
        [MaxLength(255)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("CLI_CEP")]
        [MaxLength(15)]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Column("CLI_ENDERECO")]
        [MaxLength(255)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Column("CLI_COMPLEMENTO_ENDERECO")]
        [MaxLength(450)]
        [Display(Name = "Complemento de Endereço")]
        public string ComplementoEndereco { get; set; }

        [Column("CLI_CELULAR")]
        [MaxLength(20)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Column("CLI_ESTADO")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Column("CLI_DATA_ALTERACAO")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }
    }
}
