
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_CARRO")]
    public class Carro : Notifies
    {
        [Column("CAR_ID")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CAR_MODELO")]
        [Display(Name = "Modelo")]
        [MaxLength(255)]
        [Required]
        public string Modelo { get; set; }

        [Column("CAR_MARCA")]
        [Display(Name = "Marca")]
        [MaxLength(150)]
        [Required]
        public string Marca { get; set; }

        // deve ser unique, será garantido via Fluent API no DbContext
        [Column("CAR_PLACA")]
        [Display(Name = "Placa")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve conter exatamente 7 caracteres.")]
        [Required]
        public string Placa { get; set; }

        [Column("CAR_ANO")]
        [Display(Name = "Ano")]
        [Required]
        public int Ano { get; set; }

        [Column("CAR_COR")]
        [Display(Name = "Cor")]
        [Required]
        [MaxLength(50)]
        public string Cor { get; set; }

        [Column("CAR_IMAGEM_URL")]
        [Display(Name = "URL da Imagem")]
        [MaxLength(500)]
        public string ImagemUrl { get; set; }

        [Display(Name = "CAR_CATEGORIA")]
        [ForeignKey("TB_CATEGORIA_CARRO")]
        [Column(Order = 1)]
        public int IdCategoria { get; set; }
        public virtual CategoriaCarro Categoria { get; set; }

        [Column("CAR_ATIVO")]
        [Display(Name = "Ativo")]
        [Required]
        public bool Ativo { get; set; } = true;

        [Column("CAR_DISPONIVEL")]
        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; }

        [Column("CAR_DATA_ALTERACAO")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        // relacionamento com Alocacao para verificar disponibilidade
        public virtual ICollection<Alocacao> Alocacoes { get; set; }
    }
}