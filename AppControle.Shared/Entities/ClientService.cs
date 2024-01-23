using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControle.Shared.Entities
{
    public class ClientService
    {
        public int Id { get; set; }

        public Product Product { get; set; } = null!;

        public int ProductId { get; set; }

        public Client Client { get; set; } = null!;

        public int ClientId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Inicio")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Dia de pagamento")]
        public int Payday { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(150, ErrorMessage = "O campo {0} deve ter no máximo {1} caractéres.")]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }
    }
}
