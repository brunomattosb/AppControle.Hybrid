using AppControle.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.Entities
{
    public class MonthlyFeeCreateDTO
    {
        [Key]
        public int Id { get; set; }

        public int? ClientId { get; set; }

        public string? Cpf_Cnpj { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Value { get; set; }

        public bool isSelected { get;set; }
        public ICollection<ClientService>? lClientService { get; set; }

    }
}
