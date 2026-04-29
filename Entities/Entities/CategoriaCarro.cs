using Entities.Notifications;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities
{
    [Table("TB_CATEGORIA_CARRO")]
    public class CategoriaCarro : Notifies
    {
        [Column("CAT_ID")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CAT_NOME")]
        [Display(Name = "Nome")]
        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }

        [Column("CAT_DESCRICAO")]
        [Display(Name = "Descrição")]
        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Column("CAT_VALOR_DIARIA")]
        [Display(Name = "Valor da Diária")]
        [Required]
        public decimal ValorDiaria { get; set; }

        [Column("CAT_ATIVO")]
        [Display(Name = "Ativo")]
        [Required]
        public bool Ativo { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<Carro> Carros { get; set; }
    }
}