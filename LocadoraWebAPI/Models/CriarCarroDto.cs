// LocadoraWebAPI/Models/CriarCarroDto.cs

using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarCarroDto
    {
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public string? ImagemUrl { get; set; }
        public int IdCategoria { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
