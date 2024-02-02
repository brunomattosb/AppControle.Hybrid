using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppControle.Shared.Enums;

namespace AppControle.Shared.Entities
{
    public class Client
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Email { get; set; }

        [Display(Name = "Nome / Razão Social")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obligatório.")]
        public string? Name { get; set; }

        [Display(Name = "Nome Fantasia")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? FantasyName { get; set; }

        [Display(Name = "RG / IE")]
        [MaxLength(19, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Rg_Ie { get; set; }

        [Display(Name = "CPF / CNPJ")]
        [MaxLength(14, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obligatório.")]
        public string? Cpf_Cnpj { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthData { get; set; }

        [Display(Name = "Gênero")]
        //[Required(ErrorMessage = "O Campo {0} é obligatório.")]
        public GenderType? Gender { get; set; }

        [Display(Name = "Saldo")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "O Campo {0} é obligatório.")]
        public decimal Balance { get; set; } = 0.0M;

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
        //[MaxLength(6, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public int AddressNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [Display(Name = "Bairro")]
        [MaxLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Neighborhood { get; set; }

        public City? City { get; set; }
        [Display(Name = "Cidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma {0}.")]
        public int CityId { get; set; }

        [Display(Name = "Telefone")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        //[MaxLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public long? PhoneNumber { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<ClientService>? lClientService { get; set; }
        public ICollection<MonthlyFee>? lMonthlyFees { get; set; }

        [Display(Name = "Mensalidades")]
        public decimal MonthlyFeeAmount => lClientService == null ? 0 : lClientService.Sum(x=>x.Product?.Price - x.Discount) ?? 0;

    }
}
