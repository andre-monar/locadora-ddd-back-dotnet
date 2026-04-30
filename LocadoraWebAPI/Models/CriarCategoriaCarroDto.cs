using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarCategoriaCarroDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Valor da Diária é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal ValorDiaria { get; set; }

        public bool Ativo { get; set; } = true;
    }
}