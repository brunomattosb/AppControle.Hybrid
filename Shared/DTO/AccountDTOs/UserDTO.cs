using Shared.Entities;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "A senha e a confirmação não são iguais.")]
        [Display(Name = "Confirmação de senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
        public string PasswordConfirm { get; set; } = null!;

        public virtual string? UserName { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? PhoneNumber { get; set; }

        [Display(Name = "CPF / CNPJ")]
        [Required(ErrorMessage = "O campo {0} é obligatório.")]
        [MaxLength(14, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Cpf_Cnpj { get; set; }

        [Display(Name = "Nome / Razão Social")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obligatório.")]
        public string? Name { get; set; }

        [Display(Name = "Nome Fantasia")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? FantasyName { get; set; }

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        public City? City { get; set; }

        [Display(Name = "Cidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Você deve selecionar um {0}.")]
        public int CityId { get; set; }

        [Display(Name = "RG / IE")]
        [MaxLength(19, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Rg_Ie { get; set; }

        [Display(Name = "Inscrição Municipal")]
        [MaxLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? IM { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        [Display(Name = "Data de Registro")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "CEP")]
        [MaxLength(8, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? AddressCep { get; set; }

        [Display(Name = "Logradouro")]
        [MaxLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Address { get; set; }

        [Display(Name = "Número")]
        [Range(1, int.MaxValue)]
        public int AddressNumber { get; set; } = 1;

        [Display(Name = "Bairro")]
        [MaxLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Neighborhood { get; set; }

    }
}
