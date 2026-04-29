// LocadoraWebAPI/Models/CriarCarroDto.cs

using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarCarroDto
    {
        [Required(ErrorMessage = "Modelo é obrigatório")]
        [MaxLength(255)]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Marca é obrigatória")]
        [MaxLength(150)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(7, MinimumLength = 7)]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Ano é obrigatório")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Cor é obrigatória")]
        [MaxLength(50)]
        public string Cor { get; set; }

        [MaxLength(500)]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        public int IdCategoria { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
