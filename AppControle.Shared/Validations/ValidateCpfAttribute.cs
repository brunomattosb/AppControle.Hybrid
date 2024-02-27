using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AppControle.API.Validations
{
    public class ValidateCpfAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
                return new ValidationResult("CPF / CNPJ não informado!");

            string cpf = value.ToString()!;

            Não deixar o usuario digitar caracteres

            if (ContainsNonNumericCharacters(cpf))
                return new ValidationResult("CPF / CNPJ inválido!");

            if(cpf.Length == 11)
            {
                int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
                int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

                string tempCpf = cpf.Substring(0, 9);
                int soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                int resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                string digito = resto.ToString();
                tempCpf += digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito += resto.ToString();

                if (cpf.EndsWith(digito))
                    return ValidationResult.Success;
            }
            else if (cpf.Length == 14)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("CPF /CNPJ inválido!");
   
        }

        private static bool ContainsNonNumericCharacters(string str) => new string(str.Where(char.IsDigit).ToArray()).Length != str.Length;

        private static bool ValidateLastDigits(string cpf) => cpf[9] == Convert.ToInt32(cpf[10]).ToString()[0] && cpf[10] == Convert.ToInt32(cpf[11]).ToString()[0];
    }
}
