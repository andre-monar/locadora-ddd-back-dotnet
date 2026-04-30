using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarClienteDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatória")]
        public DateOnly DataNascimento { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório")]
        [StringLength(11, MinimumLength = 10)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(8, MinimumLength = 8)]
        public string? CEP { get; set; }

        [MaxLength(255)]
        public string? Endereco { get; set; }

        [MaxLength(450)]
        public string? ComplementoEndereco { get; set; }

        public bool Ativo { get; set; } = true;
    }
}