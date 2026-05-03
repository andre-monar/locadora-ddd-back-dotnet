using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarCategoriaCarroDto
    {
        public string Nome { get; set; }

        public string? Descricao { get; set; }
        public decimal ValorDiaria { get; set; }

        public bool Ativo { get; set; } = true;
    }
}