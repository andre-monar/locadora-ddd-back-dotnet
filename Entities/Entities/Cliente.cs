using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Entities
{
    [Table("TB_CLIENTE")]
    public class Cliente : Notifies
    {
        [Column("CLI_ID")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CLI_NOME")]
        [MaxLength(150)]
        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        [Column("CLI_CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter exatamente 11 caracteres.")]
        [Display(Name = "CPF")]
        [Required]
        public string CPF { get; set; }

        [Column("CLI_DATA_NASCIMENTO")]
        [Display(Name = "Data de Nascimento")]
        [Required]
        public DateTime DataNascimento { get; set; }

        [Column("CLI_CELULAR")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Celular deve ter entre 10 e 11 dígitos")]
        [Display(Name = "Celular")]
        [Required]
        public string Celular { get; set; }

        [Column("CLI_EMAIL")]
        [MaxLength(255)]
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [Column("CLI_CEP")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve conter exatamente 8 caracteres.")]
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

        [Column("CLI_ATIVO")]
        [Display(Name = "Ativo")]
        [Required]
        public bool Ativo { get; set; }

        [Column("CLI_DATA_CRIACAO")]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Column("CLI_DATA_ALTERACAO")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        // Relacionamento 1:N com Alocacao
        public virtual ICollection<Alocacao> Alocacoes { get; set; }    
    }
}
