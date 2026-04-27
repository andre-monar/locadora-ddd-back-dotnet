using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            Notificacoes = new List<Notifies>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notificacoes;

        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;

        }

        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;

        }

        public bool ValidarPlaca(string placa, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Placa é obrigatória",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            // Remove caracteres especiais (espaços, traços)
            placa = placa.Replace("-", "").Replace(" ", "").ToUpper();

            // Formato antigo: AAA-1234 (3 letras, 4 números)
            // Formato Mercosul: AAA1A23 (3 letras, 1 número, 1 letra, 2 números)
            string padraoAntigo = @"^[A-Z]{3}[0-9]{4}$";
            string padraoMercosul = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(placa, padraoAntigo) &&
                !System.Text.RegularExpressions.Regex.IsMatch(placa, padraoMercosul))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Placa inválida. Use formato AAA-1234 ou AAA1A23",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        // Validação para data NÃO obrigatória (pode ser null)
        public bool ValidarDataOpcional(DateTime? data, string nomePropriedade)
        {
            if (data.HasValue && data.Value > DateTime.Now)
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Data não pode ser futura",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        // Validação para data obrigatória
        public bool ValidarDataObrigatoria(DateTime data, string nomePropriedade)
        {
            if (data == DateTime.MinValue)
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            if (data > DateTime.Now)
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Data não pode ser futura",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        // Validar se uma data é maior que outra (ex: dataDevolução > dataRetirada)
        public bool ValidarDataMaior(DateTime dataMenor, DateTime dataMaior, string nomePropriedade)
        {
            if (dataMaior <= dataMenor)
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = $"Data de {nomePropriedade} deve ser maior que a data anterior",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        public bool ValidarCPF(string cpf, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "CPF é obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "CPF deve conter 11 dígitos",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            // Validação do dígito verificador (algoritmo do CPF)
            if (!IsCpfValid(cpf))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "CPF inválido",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        private bool IsCpfValid(string cpf)
        {
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito = resto < 2 ? 0 : 11 - resto;

            string digitoStr = digito.ToString();
            tempCpf += digitoStr;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            digito = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito.ToString());
        }

        public bool ValidarCNH(string cnh, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(cnh))
            {
                NotifyError(nomePropriedade, "CNH é obrigatória");
                return false;
            }

            // Remove caracteres não numéricos
            cnh = new string(cnh.Where(char.IsDigit).ToArray());

            if (cnh.Length != 11)
            {
                NotifyError(nomePropriedade, "CNH deve conter 11 dígitos");
                return false;
            }

            // Algoritmo de validação da CNH
            if (!IsCnhValid(cnh))
            {
                NotifyError(nomePropriedade, "CNH inválida");
                return false;
            }

            return true;
        }

        private bool IsCnhValid(string cnh)
        {
            if (cnh.All(c => c == cnh[0])) return false;

            int soma = 0, soma2 = 0;
            int peso = 9;
            int digito1, digito2;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cnh[i].ToString()) * peso;
                soma2 += int.Parse(cnh[i].ToString()) * (9 - i);
                peso--;
            }

            digito1 = soma % 11;
            digito1 = digito1 >= 10 ? 0 : digito1;

            soma2 += digito1 * 9;
            digito2 = soma2 % 11;
            digito2 = digito2 >= 10 ? 0 : digito2;

            return (digito1 == int.Parse(cnh[9].ToString()) && 
                    digito2 == int.Parse(cnh[10].ToString()));
        }

        // ========== VALIDAÇÃO DE CEP ==========
        public bool ValidarCEP(string cep, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "CEP obrigatório",
                    NomePropriedade = nomePropriedade
                });
            }

            // Remove caracteres não numéricos
            cep = new string(cep.Where(char.IsDigit).ToArray());

            if (cep.Length != 8)
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "CEP deve conter 8 dígitos",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        // ========== VALIDAÇÃO DE CELULAR ==========
        public bool ValidarCelular(string celular, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(celular))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "Celular obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            // Remove caracteres não numéricos
            celular = new string(celular.Where(char.IsDigit).ToArray());

            // Aceita 10 dígitos (com DDD sem 9) ou 11 dígitos (com DDD e 9)
            if (celular.Length != 10 && celular.Length != 11)
            {
                Notificacoes.Add(new Notifies { mensagem = "Celular deve conter 10 ou 11 dígitos, com DDD", NomePropriedade = nomePropriedade });
                return false;
            }

            return true;
        }

        // ========== VALIDAÇÃO DE EMAIL ==========
        public bool ValidarEmail(string email, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Notificacoes.Add(new Notifies
                {
                    mensagem = "E-mail obrigatório!",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                {
                    Notificacoes.Add(new Notifies { mensagem = "E-mail inválido", NomePropriedade = nomePropriedade });
                    return false;
                }
            }
            catch
            {
                Notificacoes.Add(new Notifies { mensagem = "E-mail inválido", NomePropriedade = nomePropriedade });
                return false;
            }

            return true;
        }
    }
}
