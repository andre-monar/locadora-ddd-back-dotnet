using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            Notificacoes = new List<Notifies>();
        }

        [NotMapped]
        [JsonIgnore]
        public string NomePropriedade { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notificacoes;

        // ─── helpers ─────────────────────────────────────────
        private void NotificationsAdd(string propriedade, string msg)
        {
            Notificacoes.Add(new Notifies
            {
                NomePropriedade = propriedade,
                Mensagem = msg
            });
        }

        // ─── validadores genéricos ───────────────────────────

        public bool ValidarString(string valor, string nomePropriedade, bool obrigatorio = true)
        {
            if (obrigatorio && string.IsNullOrWhiteSpace(valor))
            {
                NotificationsAdd(nomePropriedade, "Campo obrigatório");
                return false;
            }
            return true;
        }
        public bool ValidarInt(int valor, string nomePropriedade, bool obrigatorio = true, int minimo = 0)
        {
            if (obrigatorio && valor < minimo)
            {
                NotificationsAdd(nomePropriedade, $"Valor deve ser no mínimo {minimo}");
                return false;
            }
            return true;
        }

        public bool ValidarDecimal(decimal valor, string nomePropriedade, bool obrigatorio = true, decimal minimo = 0)
        {
            if (obrigatorio && valor < minimo)
            {
                NotificationsAdd(nomePropriedade, $"Valor deve ser no mínimo {minimo}");
                return false;
            }
            return true;
        }

        public bool ValidarPlaca(string placa, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                NotificationsAdd(nomePropriedade, "Placa obrigatória");
                return false;
            }

            placa = placa.Replace("-", "").Replace(" ", "").ToUpper();
            if (!Regex.IsMatch(placa, @"^[A-Z]{3}[0-9]{4}$") &&
                !Regex.IsMatch(placa, @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$"))
            {
                NotificationsAdd(nomePropriedade, "Placa inválida. Use AAA1234 ou AAA1A23");
                return false;
            }
            return true;
        }

        public bool ValidarData(DateTime data, string nomePropriedade, bool obrigatorio = true)
        {
            if (obrigatorio && data == DateTime.MinValue)
            {
                NotificationsAdd(nomePropriedade, "Campo obrigatório");
                return false;
            }

            if (data > DateTime.Now)
            {
                NotificationsAdd(nomePropriedade, "Data não pode ser futura");
                return false;
            }
            return true;
        }


        // Validar se uma data é maior que outra (ex: dataDevolução > dataRetirada)
        public bool ValidarDataMaior(DateTime dataMenor, DateTime dataMaior, string nomePropriedade)
        {
            if (dataMaior <= dataMenor)
            {
                NotificationsAdd(nomePropriedade, $"Data de {nomePropriedade} deve ser maior que a data anterior");
                return false;
            }
            return true;
        }


        public bool ValidarCPF(string cpf, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                NotificationsAdd(nomePropriedade, "CPF obrigatório");
                return false;
            }

            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length != 11)
            {
                NotificationsAdd(nomePropriedade, "CPF deve conter 11 dígitos");
                return false;
            }
            if (!IsCpfValid(cpf))
            {
                NotificationsAdd(nomePropriedade, "CPF inválido");
                return false;
            }
            return true;
        }

        private bool IsCpfValid(string cpf)
        {
            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp = cpf.Substring(0, 9);
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(temp[i].ToString()) * mult1[i];

            int resto = soma % 11;
            int d1 = resto < 2 ? 0 : 11 - resto;
            temp += d1;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(temp[i].ToString()) * mult2[i];

            resto = soma % 11;
            int d2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(d1.ToString() + d2.ToString());
        }

        // ========== VALIDAÇÃO DE CEP ==========
        public bool ValidarCEP(string cep, string nomePropriedade, bool obrigatorio = true)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                if (obrigatorio)
                    NotificationsAdd(nomePropriedade, "CEP obrigatório");
                return !obrigatorio;
            }

            cep = new string(cep.Where(char.IsDigit).ToArray());
            if (cep.Length != 8)
            {
                NotificationsAdd(nomePropriedade, "CEP deve conter 8 dígitos");
                return false;
            }
            return true;
        }

        // ========== VALIDAÇÃO DE CELULAR ==========
        public bool ValidarCelular(string celular, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(celular))
            {
                NotificationsAdd(nomePropriedade, "Celular obrigatório");
                return false;
            }

            celular = new string(celular.Where(char.IsDigit).ToArray());
            if (celular.Length < 10 || celular.Length > 11)
            {
                NotificationsAdd(nomePropriedade, "Celular deve ter 10 ou 11 dígitos (com DDD)");
                return false;
            }
            return true;
        }


        // ========== VALIDAÇÃO DE EMAIL ==========
        public bool ValidarEmail(string email, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                NotificationsAdd(nomePropriedade, "E-mail obrigatório");
                return false;
            }
            try
            {
                var NotificationsAddr = new System.Net.Mail.MailAddress(email);
                if (NotificationsAddr.Address != email)
                {
                    NotificationsAdd(nomePropriedade, "E-mail inválido");
                    return false;
                }
            }
            catch
            {
                NotificationsAdd(nomePropriedade, "E-mail inválido");
                return false;
            }
            return true;
        }
    }
}
