using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraWebAPI.Models
{
    public class CriarClienteDto
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateOnly DataNascimento { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string? CEP { get; set; }

        public string? Endereco { get; set; }

        public string? ComplementoEndereco { get; set; }

        public bool Ativo { get; set; } = true;
    }
}